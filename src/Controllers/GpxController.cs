using System;
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
        public IEnumerable<double> UploadGpx([FromForm] GpxFileUploadModel rawFile)
        {
            using var gpxStream = rawFile.File.OpenReadStream();
            using var xmlReader = XmlReader.Create(gpxStream);
            var gpx = GpxFile.ReadFrom(xmlReader, null);

            var result = new LinkedList<double>();

            GpxWaypoint? prevWaypoint = null;
            foreach (var track in gpx.Tracks)
            {
                foreach (var segment in track.Segments)
                {
                    foreach (var waypoint in segment.Waypoints)
                    {
                        if (prevWaypoint != null)
                        {

                            var distance = GetDistance(waypoint, prevWaypoint);
                            var delay = waypoint.TimestampUtc - prevWaypoint.TimestampUtc; //TODO add null check in input
                            if (delay.HasValue)
                                result.AddLast(distance / delay.Value.Seconds);

                        }
                        prevWaypoint = waypoint;
                    }
                }
            }
            return result;
        }

        private double GetDistance(GpxWaypoint current, GpxWaypoint old)
        {
            const double radius = 6378100; // meters

            var lat1 = (Math.PI / 180) * current.Latitude.Value;
            var lat2 = (Math.PI / 180) * old.Latitude.Value;
            var lon1 = (Math.PI / 180) * current.Longitude.Value;
            var lon2 = (Math.PI / 180) * old.Longitude.Value;

            var sdlat = Math.Sin((lat2 - lat1) / 2);
            var sdlon = Math.Sin((lon2 - lon1) / 2);

            var q = Math.Pow(sdlat, 2) + Math.Cos(lat1) * Math.Cos(lat2) * sdlon * sdlon;
            var d = 2 * radius * Math.Asin(Math.Sqrt(q));

            return d;
        }

    }

}
