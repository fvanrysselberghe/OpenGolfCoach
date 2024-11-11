using NetTopologySuite.IO;

namespace OpenGolfCoach.Application.UnitTests;

public class FromGpxImplementationTest
{
    public static string SinglePointGpxContent = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<gpx xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://www.topografix.com/GPX/1/1"" xsi:schemaLocation=""http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd"" version=""1.1"" creator=""OpenGolfCoach Test"">
	<trk>
		<trkseg>
			<trkpt lat=""51.1648918823313"" lon=""4.571061806201562"">
				<ele>6.48195009957999</ele>
				<time>2024-09-14T15:06:55Z</time>
			</trkpt>
        </trkseg>
    </trk>
</gpx>";

    [Fact]
    public void GivenSinglePointGpxClosestCourseIsReturned()
    {
        var underTest = new OpenGolfCoach.Application.FromGpxImplementation();
        var singlePointGpx = GpxFile.Parse(SinglePointGpxContent, null);

        var result = underTest.Create(singlePointGpx, null);

        Assert.Equal(result.Count(), 1);
    }
}