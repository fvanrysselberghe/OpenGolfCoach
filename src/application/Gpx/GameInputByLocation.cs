using System.Collections.Generic;
using NetTopologySuite.Geometries;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application.Gpx;

public record GameInputByLocation(string GameId, GolfCourse Course, List<Coordinate> StrokeLocations) { }