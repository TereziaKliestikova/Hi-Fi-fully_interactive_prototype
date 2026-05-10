using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HIPA_BE.Models.SampleImageAnnotationModels
{
    public class GeoJsonFeatureCollectionDto
    {
        public string Type { get; set; }
        public List<Feature> Features { get; set; }
    }

    public class Feature
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public Geometry Geometry { get; set; }
        public Properties Properties { get; set; }
    }

    public class Geometry
    {
        public string Type { get; set; }
        public List<List<List<float>>> Coordinates { get; set; }
    }

    public class Properties
    {
        public string ObjectType { get; set; }
        public string? Name { get; set; }
        public Classification? Classification { get; set; }
        public Metadata? Metadata { get; set; }
    }

    public class Classification
    {
        public string Name { get; set; }
        public List<int> Color { get; set; }
    }

    public class Metadata
    {
        public string? ANNOTATION_DESCRIPTION { get; set; }
    }
}