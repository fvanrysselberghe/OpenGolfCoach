using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Moq;
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
            var overpassLine = new List<Geometry>();
            foreach (var point in hole.TargetLine)
            {
                overpassLine.Add(new Geometry
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
            ClubName = "More progress"
        };

        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = JsonContent.Create(ToOverpass(expected));

        var mock = new Mock<HttpMessageHandler>();
        mock.As<IExposedHttpMessageHandler>()
        .Setup(mockedHandler => mockedHandler.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()).Result).Returns(response);

        HttpClient client = new(mock.Object);

        var tested = new OverpassApiGolfCourseRetriever(client);

        var course = tested.Retrieve(new NetTopologySuite.Geometries.Coordinate(0, 0));

        Assert.Equal(expected, course);
    }
}