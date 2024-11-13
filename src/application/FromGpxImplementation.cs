using NetTopologySuite.IO;
using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application;

public class FromGpxImplementation : IFromGpxImplementation
{
    public FromGpxImplementation(IGolfCourseRepository courses) => _Repository = courses;

    public IEnumerable<WaypointCandidate> Create(GpxFile gpx, double? maxSpeedForStroke)
    {
        var filter = new PauseFilter();

        if (maxSpeedForStroke.HasValue)
            filter.MaxSpeedForPause = maxSpeedForStroke.Value;

        return filter.Apply(gpx);
    }

    private readonly IGolfCourseRepository _Repository;
}