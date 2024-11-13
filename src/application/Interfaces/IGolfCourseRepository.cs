using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application.Interfaces;

public interface IGolfCourseRepository
{
    public GolfCourse Retrieve(double longitude, double latitude);
}