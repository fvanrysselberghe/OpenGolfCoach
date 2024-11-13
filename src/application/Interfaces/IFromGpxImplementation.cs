using OpenGolfCoach.Application.Models;
using NetTopologySuite.IO;

namespace OpenGolfCoach.Application.Interfaces;

public interface IFromGpxImplementation
{
    public IEnumerable<WaypointCandidate> Create(GpxFile gpx, double? maxSpeedForStroke);
}