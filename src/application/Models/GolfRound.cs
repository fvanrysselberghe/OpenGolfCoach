namespace OpenGolfCoach.Application.Models;

/// <summary>
/// Represents a past round of golf. 
/// It contains all relevant data to qualify your game via metrics like "Strokes Gained". 
/// </summary>
public record class GolfRound
{
    /// <summary>
    /// Unique identification of the round
    /// </summary> 
    public required string ID { get; set; }

    /// <summary>
    /// Name of the course where the round was played 
    /// </summary>
    /// <value></value>
    public required string CourseName { get; set; }

    /// <summary>
    /// The day which the golf round took place. 
    /// It serves as a logical key for the end-user.
    /// Note that we assume you only play one round of golf a day. 
    /// </summary>
    public required DateOnly Day { get; set; }

}