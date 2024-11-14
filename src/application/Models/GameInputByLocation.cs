using System.Collections.Generic;

namespace OpenGolfCoach.Application.Models;

public record GameInputByLocation(string GameId, GolfCourse Course, List<WaypointCandidate> StrokeLocations) { }