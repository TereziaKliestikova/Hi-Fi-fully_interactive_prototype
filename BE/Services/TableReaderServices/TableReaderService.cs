using HIPA_BE.Models.TableParsing;
using static HIPA_BE.Services.TableReaderServices.TableReaderService;

namespace HIPA_BE.Services.TableReaderServices
{
    public interface ITableReaderService
    {
        bool CanRead(string fileName, string? contentType = null);
        Task<TableReadResult> ReadAsync(Stream file, string fileName, CancellationToken ct = default);
        Task<TableParsingResult<T>> ParseTableAsync<T>(Stream file, string fileName, string parsingType, CancellationToken ct = default) where T : class;
    }

    public class TableReaderService : ITableReaderService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TableReaderService> _logger;

        public record TableCell(int Row, int Col, string? Value);
        public record TableRow(int Row, IReadOnlyDictionary<string, string?> Cells);
        public record TableMatrix(string? Sheet, int RowCount, int ColumnCount, List<TableRow> Rows);
        public record TableReadResult(string? Sheet, IReadOnlyList<string> Headers, IReadOnlyList<TableRow> Rows);
        public record TableParsingResult<T>(IReadOnlyList<T> Items, IReadOnlyList<string> Errors);

        private static readonly HashSet<string> SupportedExtensions = [".xlsx", ".xls", ".xlsb", ".csv", ".tsv"];

        public TableReaderService(IConfiguration configuration, ILogger<TableReaderService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public bool CanRead(string fileName, string? contentType = null) =>
            SupportedExtensions.Contains(Path.GetExtension(fileName).ToLowerInvariant());

        public async Task<TableReadResult> ReadAsync(Stream file, string fileName, CancellationToken ct = default)
        {
            var matrix = await ReadToMatrix(file, fileName, ct);
            return new TableReadResult(
                matrix.Sheet,
                Enumerable.Range(0, matrix.ColumnCount).Select(i => i.ToString()).ToList(),
                matrix.Rows 
            );
        }

        private async Task<TableMatrix> ReadToMatrix(Stream file, string fileName, CancellationToken ct)
        {
            using var mem = new MemoryStream();
            await file.CopyToAsync(mem, ct);
            mem.Position = 0;

            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            var rows = new List<TableRow>();
            int maxColumns;

            if (extension is ".csv" or ".tsv")
            {
                maxColumns = await ReadCsvToMatrix(mem, extension == ".tsv", rows);
            }
            else
            {
                maxColumns = await ReadExcelToMatrix(mem, rows);
            }

            return new TableMatrix("Sheet1", rows.Count, maxColumns, rows);
        }

        private async Task<int> ReadExcelToMatrix(Stream file, List<TableRow> rows)
        {
            using var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(file);
            int rowIndex = 0;
            int maxColumns = 0;

            while (reader.Read())
            {
                var dict = new Dictionary<string, string?>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var value = reader.GetValue(i);
                    dict[i.ToString()] = value?.ToString()?.Trim();
                }

                rows.Add(new TableRow(rowIndex++, dict));
                maxColumns = Math.Max(maxColumns, reader.FieldCount);
            }

            return maxColumns;
        }

        private async Task<int> ReadCsvToMatrix(Stream file, bool isTsv, List<TableRow> rows)
        {
            using var reader = new StreamReader(file);
            using var csv = new CsvHelper.CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                Delimiter = isTsv ? "\t" : ",",
                HasHeaderRecord = false
            });

            int rowIndex = 0;
            int maxColumns = 0;

            while (await csv.ReadAsync())
            {
                var dict = new Dictionary<string, string?>();
                for (int i = 0; csv.TryGetField(i, out string? value); i++)
                {
                    dict[i.ToString()] = value?.Trim();
                    maxColumns = Math.Max(maxColumns, i + 1);
                }

                rows.Add(new TableRow(rowIndex++, dict));
            }

            return maxColumns;
        }

        public async Task<TableParsingResult<T>> ParseTableAsync<T>(Stream file, string fileName, string parsingType, CancellationToken ct = default) where T : class
        {
            var matrix = await ReadToMatrix(file, fileName, ct);
            return FilterAndMap<T>(matrix, parsingType);
        }

        private TableParsingResult<T> FilterAndMap<T>(TableMatrix matrix, string parsingType) where T : class
        {
            var config = _configuration.GetSection($"TableParsing:{parsingType}")
                .Get<TableParsingConfig>() ?? throw new ArgumentException($"No configuration found for {parsingType}");

            var items = new List<T>();
            var errors = new List<string>();

            foreach (var row in matrix.Rows)
            {
                try
                {
                    if (row.Row < config.DataStartRow - 1)
                        continue;

                    var mappedValues = new Dictionary<string, string?>();
                    foreach (var (colIndexStr, propertyName) in config.ColumnMap)
                    {
                        if (row.Cells.TryGetValue(colIndexStr, out var value))
                        {
                            mappedValues[propertyName] = value;
                        }
                    }

                    if (mappedValues.Values.Any(v => !string.IsNullOrWhiteSpace(v)))
                    {
                        try
                        {
                            var item = MapToType<T>(mappedValues);
                            items.Add(item);
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Error mapping row {row.Row + 1}: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Error processing row {row.Row + 1}: {ex.Message}");
                }
            }

            return new TableParsingResult<T>(items, errors);
        }

        private static T MapToType<T>(Dictionary<string, string?> values) where T : class
        {
            var instance = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                if (values.TryGetValue(prop.Name, out var value) && value != null)
                {
                    try
                    {
                        var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                        var convertedValue = Convert.ChangeType(value, targetType);
                        prop.SetValue(instance, convertedValue);
                    }
                    catch (Exception)
                    {
                        throw new Exception($"Failed to convert value '{value}' to type {prop.PropertyType.Name} for property '{prop.Name}'.");
                    }
                }
            }

            return instance;
        }
    }
}