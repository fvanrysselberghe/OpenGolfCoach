using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Algorithm;
using NetTopologySuite.Geometries;

namespace OpenGolfCoach.Application.Models
{

    public sealed record Hole
    {
        /// <summary>
        /// Sequence number of the hole in the course
        /// </summary>
        public int Number { get; init; }

        /// <summary>
        ///  PAR rating of the hole. 
        /// </summary>
        public int Par { get; init; }

        /// <summary>
        /// Strokeindex i.e., the difficulty ranking of the hole in the course
        /// </summary>
        public int Strokeindex { get; init; }

        /// <summary>
        /// Ideal line from teebox to pin-position
        /// </summary>
        public IEnumerable<Coordinate> TargetLine { get; init; } = [];

        /// <summary>
        /// The different elements that make up the golf course
        /// Includes roughs, fairways, bunkers, green, ...
        /// </summary>
        public IEnumerable<GolfCourseArea> Parts { get; init; } = [];
    }
}