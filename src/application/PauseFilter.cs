using NetTopologySuite.IO;
using OpenGolfCoach.Application.Models;

namespace OpenGolfCoach.Application
{
    /// <summary>
    /// Filters (possible) stops from a set of Gpx Tracks
    /// </summary>
    public class PauseFilter
    {
        /// <summary>
        /// Speeds below this value are considered pauses
        /// </summary>
        public double MaxSpeedForPause { get; set; } = 0.2;

        /// <summary>
        /// Applies the filter on the given GpxFile returning the likely stops
        /// </summary>
        /// <param name="file">Content to retrieve the stops</param>
        /// <returns>Likely stops</returns>
        public IEnumerable<WaypointCandidate> Apply(GpxFile file)
        {
            var result = new LinkedList<WaypointCandidate>();

            GpxWaypoint? prevWaypoint = null;
            foreach (var track in file.Tracks)
            {
                foreach (var segment in track.Segments)
                {
                    foreach (var waypoint in segment.Waypoints)
                    {
                        if (prevWaypoint == null)
                        {
                            // First waypoint hence we don't have a speed yet.
                            result.AddLast(new WaypointCandidate { Speed = 0, Longitude = waypoint.Longitude.Value, Latitude = waypoint.Latitude.Value });
                        }
                        else
                        {
                            // Succeeding waypoints hence determine speed by checking the distance covered in the elapsed time
                            var distance = GetDistance(waypoint, prevWaypoint);
                            var delay = waypoint.TimestampUtc - prevWaypoint.TimestampUtc; //TODO add null check in input
                            if (delay.HasValue)
                                result.AddLast(new WaypointCandidate { Speed = distance / delay.Value.Seconds, Longitude = prevWaypoint.Longitude.Value, Latitude = prevWaypoint.Latitude.Value });
                        }
                        prevWaypoint = waypoint;
                    }
                }
            }

            return result.Where(candidate => candidate.Speed <= MaxSpeedForPause);
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