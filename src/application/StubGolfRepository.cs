using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application;
public class StubGolfRepository : IGolfCourseRepository
{
    public GolfCourse Retrieve(double longitude, double latitude)
    {
        return new GolfCourse("Bushwood Country Club");
    }
}