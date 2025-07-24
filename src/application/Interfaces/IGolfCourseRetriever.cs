using NetTopologySuite.Geometries;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application.Interfaces;

public interface IGolfCourseRetriever
{
    /// <summary>
    /// Retrieves the courses which are close to the provided location.
    /// The most likely candidate (i.e., closest to the target location) is returned first. 
    /// </summary>
    /// <param name="location"></param>
    /// <param name="maxCandidates">
    /// <returns></returns>
    public Task<GolfCourse> Retrieve(Coordinate location);

}