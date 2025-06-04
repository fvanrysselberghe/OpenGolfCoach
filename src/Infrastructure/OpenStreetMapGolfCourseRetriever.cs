using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;
using NetTopologySuite.Geometries;

namespace OpenGolfCoach.Infrastructure.OpenStreetmap;

public class GolfCourseRetriever : IGolfCourseRetriever
{
    public GolfCourse Retrieve(Coordinate coordinate)
    {
        return new GolfCourse()
        {
            Name = "Work in progress",
            ClubName = "More progress"
        };
    }
}