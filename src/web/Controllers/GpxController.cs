using System;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.Xml;
using NetTopologySuite.IO;
using OpenGolfCoach.Application;
using OpenGolfCoach.Application.Interfaces;
using OpenGolfCoach.Application.Models;
using OpenGolfCoach.Web.Models;

namespace OpenGolfCoach
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GpxController : ControllerBase
    {
        public GpxController(IFromGpxImplementation fromGpxImplementation) => _fromGpxImplementation = fromGpxImplementation;

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
        public IActionResult CreateFromGpx([FromForm] GpxFileUploadModel rawFile, double? maxSpeedForStroke)
        {
            using var gpxStream = rawFile.File.OpenReadStream();
            using var xmlReader = XmlReader.Create(gpxStream);
            var gpx = GpxFile.ReadFrom(xmlReader, null);
            try
            {
                return Ok(_fromGpxImplementation.Create(gpx, maxSpeedForStroke));
            }
            catch (NoLocationException)
            {
                return BadRequest(); //May require more details for the frontend to handle
            }
        }

        private readonly IFromGpxImplementation _fromGpxImplementation;

    }
}
