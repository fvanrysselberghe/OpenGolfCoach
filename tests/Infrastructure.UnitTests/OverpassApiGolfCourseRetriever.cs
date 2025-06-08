using OpenGolfCoach.Infrastructure.OpenStreetmap;
using System.Net;

namespace Infrastructure.UnitTests;

public class OpenStreetMapGolfCourseRetrieverTest
{
    [Fact]
    public void WhenSingleCandidateReturnAsCourse()
    {
        HttpMessageHandler handler = new HttpClientHandler();
        HttpClient client = new(handler);

        var tested = new OverpassApiGolfCourseRetriever(client);
    }
}