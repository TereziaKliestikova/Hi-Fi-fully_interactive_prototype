using Microsoft.EntityFrameworkCore;
using HIPA_BE.Models.SampleImageAnnotationModels;
using System.Text.Json;

namespace HIPA_BE.Data.Seeding
{
    public class SampleImageAnnotationSeeding
    {
        static string[] geojsonPath = new string[] {"assets", "geojson"};

        public static List<SampleImageAnnotation> GetModels(int organN)
        {
            int id = 1;
            int sampleImagesN = organN * 2;
            // Read content of two geojson files from a folder
            // load the content to a string
            List<GeoJsonFeatureCollectionDto> geojsonAnnotations = GetGeojsonFiles(Path.Combine(geojsonPath));
            // initialize list of certain length
            List<SampleImageAnnotation> sampleImageAnnotations = new List<SampleImageAnnotation>(geojsonAnnotations.Count * sampleImagesN);

            // this loop "picks" which geojson file to use
            for (int sampleImageId = 1; sampleImageId <= sampleImagesN; sampleImageId++)
            {
                // pick list of annotations from a specific annotation file
                var annotations = geojsonAnnotations[sampleImageId % geojsonAnnotations.Count].Features;
                // this loop assigns the annotations to the sample images
                for (int annotIdx = 0; annotIdx < annotations.Count; annotIdx++)
                {
                    // select specific annotation
                    var annot = annotations[annotIdx];
                    var sampleImageAnnotation = new SampleImageAnnotation()
                    {
                        ID = id++,
                        Name = annot.Properties.Classification.Name,
                        Description = $"Anotácia {sampleImageId} Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                        SampleImageID = sampleImageId,
                        BoundingBox = JsonSerializer.Serialize(annot)
                    };
                    // append to sampleImageAnnotations
                    sampleImageAnnotations.Add(sampleImageAnnotation);
                }
            }
            return sampleImageAnnotations;
        }

        private static List<GeoJsonFeatureCollectionDto> GetGeojsonFiles(string directoryPath)
        {
            // Get all files in the directory
            string[] files = Directory.GetFiles(directoryPath);

            // Array to for lists of annotations for each file
            List<GeoJsonFeatureCollectionDto> featureCollections = new List<GeoJsonFeatureCollectionDto>();

            // sort files by file name
            // to ensure the same order of files
            // as are seeded to images in the database
            Array.Sort(files);

            // Loop through each file
            for (int i = 0; i < files.Length; i++)
            {
                // Read the JSON file into a string
                string jsonString = File.ReadAllText(files[i]);

                // Deserialize the JSON string into a FeatureCollection object
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // So that it is correctly mapped to the DTO structure
                };
                GeoJsonFeatureCollectionDto featureCollection = JsonSerializer.Deserialize<GeoJsonFeatureCollectionDto>(jsonString, options);

                featureCollections.Add(featureCollection);
            }
            return featureCollections;
        }
    }
}