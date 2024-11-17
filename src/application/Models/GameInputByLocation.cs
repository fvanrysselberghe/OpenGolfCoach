using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace OpenGolfCoach.Application.Models;

public record GameInputByLocation(string GameId, GolfCourse Course, List<Coordinate> StrokeLocations) { }