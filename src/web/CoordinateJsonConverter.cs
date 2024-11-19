using System.Text.Json;
using System.Text.Json.Serialization;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace OpenGolfCoach.Web;

/// <summary>
/// Converts (NetTopologySuite) Coordinates to json. 
/// </summary>
class CoordinateJsonConverter : JsonConverter<Coordinate>
{
    public override Coordinate? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    // { 'longitude' = '', 'latitude'=''}
    public override void Write(Utf8JsonWriter writer, Coordinate value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteNumber("longitude", value.X);
        writer.WriteNumber("latitude", value.Y);

        writer.WriteEndObject();
    }
}