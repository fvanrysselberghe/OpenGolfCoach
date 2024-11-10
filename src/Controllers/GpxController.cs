using System;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.Xml;
using NetTopologySuite.IO;
using OpenGolfCoach.Models;

namespace OpenGolfCoach
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GpxController : ControllerBase
    {
        /// <summary>
        /// Creates a game record from a GPX file. 
        /// The game record is largely empty since the processing of the GpxFile requires manual intervention
        /// Only basic properties like the date and (likely) location are filled in. 
        // 
        ///  
        /// </summary>
        /// <param name="rawFile"></param>
        /// <param name="maxSpeedForStroke"></param>
        /// <returns>Data needed for the manual entry</returns>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public IEnumerable<WaypointCandidate> CreateFromGpx([FromForm] GpxFileUploadModel rawFile, double? maxSpeedForStroke)
        {
            using var gpxStream = rawFile.File.OpenReadStream();
            using var xmlReader = XmlReader.Create(gpxStream);
            var gpx = GpxFile.ReadFrom(xmlReader, null);

            var filter = new PauseFilter();

            if (maxSpeedForStroke.HasValue)
                filter.MaxSpeedForPause = maxSpeedForStroke.Value;

            return filter.Apply(gpx);
        }
    }
}
