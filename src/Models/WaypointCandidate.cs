using NetTopologySuite.IO;

namespace OpenGolfCoach.Models;

public record class WaypointCandidate
{
    public required GpxWaypoint Waypoint { init; get; }
    public double Speed { init; get; }
}