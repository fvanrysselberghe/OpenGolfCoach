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

        if (attempts.Count() < 1)
            throw new NoLocationException();

        var referencePoint = GetReferencePoint(attempts);
        return new GameInputByLocation("123", _Repository.Retrieve(referencePoint.Item1, referencePoint.Item2), new List<WaypointCandidate>(attempts));
    }

    /// <summary>
    /// Determines a reference point (i.e., location) that can be used to determine the course which is being played.
    /// From all possible approaches (most central, first, last...), we currently implement the simplest i.e., the first
    /// point. We thus assume that the user starts tracking his locations at the start of his game. 
    /// 
    /// Pre-condition: attempts is not empty
    /// </summary>
    /// <param name="attempts">Locations tracked</param>
    /// <returns></returns>
    private Tuple<double, double> GetReferencePoint(IEnumerable<WaypointCandidate> attempts)
    {
        var first = attempts.First();
        return new Tuple<double, double>(first.Longitude, first.Latitude);
    }

    private readonly IGolfCourseRepository _Repository;
}