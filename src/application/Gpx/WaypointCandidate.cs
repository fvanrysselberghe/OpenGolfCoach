using NetTopologySuite.Geometries;

namespace OpenGolfCoach.Application.Gpx;

public record class WaypointCandidate
{
    public double Speed { init; get; }

    public Coordinate Location { get { return new Coordinate(Longitude, Latitude); } }

    public required double Longitude { init; get; }
    public required double Latitude { init; get; }
}