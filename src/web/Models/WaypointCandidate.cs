using NetTopologySuite.IO;

namespace OpenGolfCoach.Models;

public record class WaypointCandidate
{
    public double Speed { init; get; }
    public required double Longitude { init; get; }
    public required double Latitude { init; get; }
}