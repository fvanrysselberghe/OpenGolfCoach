using NetTopologySuite.Geometries;
using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application.UnitTests;

public class StubGolfRepository : IGolfCourseRetriever
{
    public GolfCourse Retrieve(Coordinate location) => new GolfCourse { Name = "Bushwood Country Club" };
}