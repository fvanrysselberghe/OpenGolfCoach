using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Moq;
using Moq.Protected;
using NetTopologySuite.Geometries;
using OpenGolfCoach.Application.Models;
using OpenGolfCoach.Infrastructure.OpenStreetmap;

namespace Infrastructure.UnitTests;

/// <summary>
/// Interface which exposes HttpMessageHandler's protected methods as public for LSP support
/// </summary>
public interface IExposedHttpMessageHandler
{
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
}

public class OpenStreetMapGolfCourseRetrieverTest
{
    private static OverpassResponse BaseResponse
    {
        get
        {
            return new OverpassResponse
            {
                Version = 0.6,
                Generator = "Overpass API 0.7.62.7 375dc00a",
                Osm3s = new OsmServiceInfo { OsmBaseTime = DateTime.Now, AreasBaseTime = DateTime.Now, Copyright = "The data included in this document is from www.openstreetmap.org. The data is made avail under ODbL." },
                Elements = []
            };
        }
    }

    private static OverpassResponse ToOverpass(GolfCourse course)
    {
        var response = OpenStreetMapGolfCourseRetrieverTest.BaseResponse;

        int uniqueNumber = 0;

        // Add club element
        response.Elements.Add(new OverpassElement
        {
            Type = "way",
            Id = ++uniqueNumber,
            Tags = new Dictionary<string, string>
            {
                ["leisure"] = "golf_course",
                ["name"] = course.Name,
            }
        });

        // Add holes
        var holeNumber = 0;
        foreach (var hole in course.Holes)
        {
            var overpassLine = new List<OpenGolfCoach.Infrastructure.OpenStreetmap.Geometry>();
            foreach (var point in hole.TargetLine)
            {
                overpassLine.Add(new OpenGolfCoach.Infrastructure.OpenStreetmap.Geometry
                {
                    Longitude = point.X,
                    Latitude = point.Y
                });
            }

            response.Elements.Add(new OverpassElement
            {
                Type = "way",
                Id = ++uniqueNumber,
                Geometry = overpassLine,
                Tags = new Dictionary<string, string>
                {
                    ["golf"] = "hole",
                    ["ref"] = holeNumber.ToString(),
                }
            });
        }

        // Add hazards and other course elements
        // ... not yet needed

        // Return json
        return response;
    }

    [Fact]
    public void WhenSingleCandidateReturnAsCourse()
    {
        var expected = new GolfCourse()
        {
            Name = "Work in progress",
            Holes = [new Hole { Number = 1, Par = 3, TargetLine = [new Coordinate(51.1651252, 4.5711302), new Coordinate(51.1653774, 4.569289)], Strokeindex = 6 },
                new Hole{Number = 2, Par = 3, TargetLine = [new Coordinate(51.1656968, 4.5685237), new Coordinate(51.1660897, 4.5675215)], Strokeindex = 8},
                new Hole{Number=3, Par=4, TargetLine = [new Coordinate(51.1666895, 4.566289), new Coordinate(51.1661327, 4.5685564), new Coordinate(51.1667731, 4.5702201)], Strokeindex = 1}]
        };

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(ToOverpass(expected))
        };

        var mock = new Mock<HttpMessageHandler>();
        mock.Protected()
            .As<IExposedHttpMessageHandler>()
            .Setup(mockedHandler => mockedHandler.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()).Result)
            .Returns(response);

        HttpClient client = new(mock.Object);

        var tested = new OverpassApiGolfCourseRetriever(client);

        var course = tested.Retrieve(new NetTopologySuite.Geometries.Coordinate(0, 0));

        Assert.Equal(expected, course);
    }
}