using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;
using System.Net;
using NetTopologySuite.Geometries;

namespace OpenGolfCoach.Infrastructure.OpenStreetmap;

/// <summary>
/// Retrieves golf courses by quering the overpass API.
/// The class thus relies on network connectivity. 
/// The class therefore risks latency and network connectivity problems.
/// </summary>
public class OverpassApiGolfCourseRetriever : IGolfCourseRetriever
{
    public OverpassApiGolfCourseRetriever(HttpClient client)
    {
        _client = client;
    }
    public GolfCourse Retrieve(Coordinate coordinate)
    {
        var query = new StringContent(CreateFetchQuery(coordinate));
        var response = _client.PostAsync(OverpassUrl, query);

        return new GolfCourse()
        {
            Name = "Work in progress"
        };
    }

    static string CreateFetchQuery(Coordinate coordinate)
    {
        return $"[out: json]; way[leisure = golf_course] (around:1000, {coordinate.X}, {coordinate.Y}) -> .courses; courses out tags bb; way(area.courses)[golf] -> .elements;.elements out geom;";
    }

    private const string OverpassUrl = "https://overpass-api.de/api/interpreter";
    private readonly HttpClient _client;
}