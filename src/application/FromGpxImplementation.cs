using System.Drawing;
using System.Net;
using NetTopologySuite.Geometries;
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

        return new GameInputByLocation("123", _Repository.Retrieve(GetReferencePoint(attempts)), new List<Coordinate>(attempts.Select(candidate => candidate.Location).ToList()));
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
    private Coordinate GetReferencePoint(IEnumerable<WaypointCandidate> attempts)
    {
        return attempts.First().Location;
    }

    private readonly IGolfCourseRepository _Repository;
}