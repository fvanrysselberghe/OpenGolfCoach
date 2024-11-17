using NetTopologySuite.Geometries;
using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application.UnitTests;
public class StubGolfRepository : IGolfCourseRepository
{
    public GolfCourse Retrieve(double longitude, double latitude)
    {
        return Retrieve(new Coordinate(longitude, latitude));
    }

    public GolfCourse Retrieve(Coordinate location)
    {
        return new GolfCourse("Bushwood Country Club");
    }
}