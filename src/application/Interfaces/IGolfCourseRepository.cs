using NetTopologySuite.Geometries;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application.Interfaces;

public interface IGolfCourseRepository
{
    /// <summary>
    /// Retrieves the GolfCourse which is closest to the provided location
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public GolfCourse Retrieve(Coordinate location);

}