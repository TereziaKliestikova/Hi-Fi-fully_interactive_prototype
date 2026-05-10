namespace HIPA_BE.Models.TableParsing
{
    public record TableParsingConfig
    {
        public required Dictionary<string, string> ColumnMap { get; init; }
        public required int DataStartRow { get; init; }
    }
}
