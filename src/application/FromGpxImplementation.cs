using NetTopologySuite.IO;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application;

public class FromGpxImplementation
{

    public IEnumerable<WaypointCandidate> Create(GpxFile gpx, double? maxSpeedForStroke)
    {
        var filter = new PauseFilter();

        if (maxSpeedForStroke.HasValue)
            filter.MaxSpeedForPause = maxSpeedForStroke.Value;

        return filter.Apply(gpx);
    }
}