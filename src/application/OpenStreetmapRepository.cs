using NetTopologySuite.Geometries;
using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application;
public class OpenStreetmapRepository : IGolfCourseRepository
{
    public GolfCourse Retrieve(double longitude, double latitude)
    {
        return Retrieve(new Coordinate(longitude, latitude));
    }
    public GolfCourse Retrieve(Coordinate location)
    {
        return new GolfCourse("Work in progress");
    }
}