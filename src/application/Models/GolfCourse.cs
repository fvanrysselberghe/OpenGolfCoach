namespace OpenGolfCoach.Application.Models;

public class GolfCourse
{
    public GolfCourse(string name) => Name = name;

    public string Name { get; set; }
}