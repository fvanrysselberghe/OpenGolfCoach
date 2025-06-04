using NetTopologySuite.Geometries;

namespace OpenGolfCoach.Application.Models
{
    /// <summary>
    /// An area on a golf course like a teebox, green or bunker.
    /// </summary>
    public sealed record GolfCourseArea
    {
        public enum AreaType
        {
            GREEN,
            BUNKER,
            ROUGH,
            TEEBOX
        }

        /// <summary>
        /// Type of area
        /// </summary>
        public AreaType Type { get; init; }

        public IEnumerable<Coordinate> Bounds { get; init; } = []; //or replace by an actual area?
    }
}