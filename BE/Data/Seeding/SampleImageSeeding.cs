using System.IO;
using HIPA_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace HIPA_BE.Data.Seeding
{
    public class SampleImageSeeding
    {
        static string[] sampleImageFiles = { "10_HE.dzi" };

        public static List<SampleImage> GetModels(int organN, int diagnosisN)
        {
            int id = 1;
            var sampleImages = new List<SampleImage>(organN);
            string sampleImageDir = "/media/wsi/db141d52-1374-43a9-8938-3d7dca588a0d/dzi";

            for (int i = 1; i <= organN; i++)
            {
                for (int j = 1; j <= 2; j++)
                {
                    // Adjust the logic for accessing organs and diagnoses
                    // plus one to get the correct diagnosis ID and avoid getting 0
                    var diagnosisIndex = (j % diagnosisN) + 1;
                    // Create a new sample image
                    sampleImages.Add(new SampleImage()
                    {
                        ID = id,
                        Name = $"OBR{i}.{j}",
                        OrganID = i,
                        DiagnosisID = diagnosisIndex,
                        Path = $"{sampleImageDir}/{sampleImageFiles[id % sampleImageFiles.Length]}",
                        LastModified = DateTime.UtcNow,
                        GroupId = "Seeding",
                        State = "READY",
                        Metadata = "{\"Magnification\":40,\"Format\":\"CellSens VSI\",\"Domains\":[\"Histology\"],\"Resolution\":0,\"FillColor\":0,\"Levels\":11,\"PixelsPerMeter\":{\"X\":6155707.442529957,\"Y\":6155688.582957196,\"Avg\":6155698.012743576},\"Size\":{\"Width\":{\"Pixel\":103140,\"Micro\":16755.18223744729},\"Height\":{\"Pixel\":134245,\"Micro\":21808.283214923234},\"Z\":1,\"C\":3,\"T\":1},\"Tile\":{\"Optimal\":{\"Width\":512,\"Height\":512}},\"Pixel\":{\"Type\":\"uint8\",\"Width\":{\"Micro\":0.1624508652069739,\"Meter\":1.624508652069739E-07},\"Height\":{\"Micro\":0.16245136291797263,\"Meter\":1.624513629179726E-07}}}",
                    });
                    id++;
                }
            }
            return sampleImages;
        }
    }
}
