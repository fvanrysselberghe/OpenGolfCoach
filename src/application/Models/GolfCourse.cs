using NetTopologySuite.Operation.Polygonize;

namespace OpenGolfCoach.Application.Models;

/// <summary>
/// Represents a golf course that consists of multiple holes.
/// A golf club can consists of multiple courses. 
/// </summary>
public sealed record GolfCourse
{
    /// <summary>
    /// Name which identifies the course.
    /// </summary>
    public string Name { get; init; } = String.Empty;

    /// <summary>
    /// Collection of holes that are part of this course. 
    /// Typically consists of 9 or 18 holes. 
    /// </summary>
    public IEnumerable<Hole> Holes { get; init; } = [];
}