using System.ComponentModel.DataAnnotations;

namespace HIPA_BE.Models.SampleImageModels
{
    public class SampleImageMetadataDto
    {
        public double Magnification { get; set; } = 0.0;
        
        public string Format { get; set; } = string.Empty;
        
        public List<string> Domains { get; set; } = new List<string>();
        
        public int Resolution { get; set; } = 0;
        
        public int FillColor { get; set; } = 0;
        
        public int Levels { get; set; } = 0;
        
        public PixelsPerMeter PixelsPerMeter { get; set; } = new();
        
        public ImageSize Size { get; set; } = new();
        
        public TileInfo Tile { get; set; } = new();
        
        public PixelInfo Pixel { get; set; } = new();
    }

    public class PixelsPerMeter
    {
        public double X { get; set; } = 0.0;
        
        public double Y { get; set; } = 0.0;
        
        public double Avg { get; set; } = 0.0;
    }

    public class ImageSize
    {
        public DimensionPixel Width { get; set; } = new();
        
        public DimensionPixel Height { get; set; } = new();
        
        public int Z { get; set; } = 1;
        
        public int C { get; set; } = 1;
        
        public int T { get; set; } = 1;
    }

    public class DimensionPixel
    {
        public int Pixel { get; set; } = 0;
        
        public double Micro { get; set; } = 0.0;
    }

    public class DimensionMeter
    {
        public double Micro { get; set; } = 0.0;
        
        public double Meter { get; set; } = 0.0;
    }

    public class TileInfo
    {
        public OptimalDimension Optimal { get; set; } = new();
    }

    public class OptimalDimension
    {
        public int Width { get; set; } = 0;
        
        public int Height { get; set; } = 0;
    }

    public class PixelInfo
    {
        public string Type { get; set; } = string.Empty;
        
        public DimensionMeter Width { get; set; } = new();
        
        public DimensionMeter Height { get; set; } = new();
    }
}