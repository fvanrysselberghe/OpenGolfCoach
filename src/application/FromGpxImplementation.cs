using System.Drawing;
using System.Net;
using NetTopologySuite.IO;
using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application;

public class FromGpxImplementation : IFromGpxImplementation
{
    public FromGpxImplementation(IGolfCourseRepository courses) => _Repository = courses;

    public GameInputByLocation Create(GpxFile gpx, double? maxSpeedForStroke)
    {
        var filter = new PauseFilter();

        if (maxSpeedForStroke.HasValue)
            filter.MaxSpeedForPause = maxSpeedForStroke.Value;

        var attempts = filter.Apply(gpx);

        return new GameInputByLocation("123", _Repository.Retrieve(0, 0), new List<WaypointCandidate>(attempts));
    }

    private readonly IGolfCourseRepository _Repository;
}