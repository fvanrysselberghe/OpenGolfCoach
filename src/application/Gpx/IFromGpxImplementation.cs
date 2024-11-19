using OpenGolfCoach.Application.Models;
using NetTopologySuite.IO;

namespace OpenGolfCoach.Application.Gpx;

public interface IFromGpxImplementation
{
    public GameInputByLocation Create(GpxFile gpx, double? maxSpeedForStroke);
}