using NetTopologySuite.Geometries;
using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application.UnitTests;

public class StubGolfRepository : IGolfCourseRetriever
{
    public Task<GolfCourse> Retrieve(Coordinate location) => Task.FromResult(new GolfCourse { Name = "Bushwood Country Club" });
}