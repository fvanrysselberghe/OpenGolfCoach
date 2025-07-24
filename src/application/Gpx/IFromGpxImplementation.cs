using OpenGolfCoach.Application.Models;
using NetTopologySuite.IO;

namespace OpenGolfCoach.Application.Gpx;

public interface IFromGpxImplementation
{
    public Task<GameInputByLocation> Create(GpxFile gpx, double? maxSpeedForStroke);
}