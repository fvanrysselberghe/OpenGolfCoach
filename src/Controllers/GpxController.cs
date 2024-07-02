using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.Xml;
using NetTopologySuite.IO;

namespace OpenGolfCoach
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GpxController : ControllerBase
    {
        [HttpPost]
        [Consumes("multipart/form-data")]
        public int Post([FromForm] GpxFileUploadModel rawFile)
        {
            using var gpxStream = rawFile.File.OpenReadStream();
            using var xmlReader = XmlReader.Create(gpxStream);
            var gpx = GpxFile.ReadFrom(xmlReader, null);

            return gpx.Tracks.Count;
        }
    }
}
