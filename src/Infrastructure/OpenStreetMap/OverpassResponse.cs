using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenGolfCoach.Infrastructure.OpenStreetmap;


/// <summary>
/// Models the response from an Overpass query
/// </summary>
public record OverpassResponse
{
    public double Version { get; init; }
    public string Generator { get; init; } = string.Empty;
    public OsmServiceInfo? Osm3s { get; init; }

    public required List<OverpassElement> Elements { get; init; }
}
public record OsmServiceInfo
{
    [JsonPropertyName("Timestamp_osm_base")]
    public DateTime OsmBaseTime { get; init; }

    [JsonPropertyName("Timestamp_areas_base")]
    public DateTime AreasBaseTime { get; init; }
    public string? Copyright { get; init; }
}

/// <summary>
/// An element in an overpass result(set). Typically refers to OSM element: node or way
/// </summary>
public record OverpassElement
{
    /// <summary>
    /// Unique identification of the element
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Type of element i.e., node or way
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// Bounding box that contains the element
    /// May be blank depending on the query
    /// </summary>
    public BoundingBox? Bounds { get; set; }

    /// <summary>
    /// Tags that are associated with the (OSM) node
    /// </summary>
    public Dictionary<string, string> Tags { get; set; } = [];
    public List<Geometry> Geometry { get; set; } = [];
}
public record BoundingBox
{
    [JsonPropertyName("minlat")]
    public double MinimumLatitude { get; init; }
    [JsonPropertyName("maxlat")]
    public double MaximumLatitude { get; init; }

    [JsonPropertyName("minlon")]
    public double MinimumLongitude { get; init; }
    [JsonPropertyName("maxlon")]
    public double MaximumLongitude { get; init; }
}

public record Geometry
{
    [JsonPropertyName("lat")]
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}