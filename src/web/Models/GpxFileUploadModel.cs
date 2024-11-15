namespace OpenGolfCoach.Web.Models;

/// <summary>
/// Models the information when uploading a (Gpx)file to the system
/// </summary> 
public class GpxFileUploadModel
{
    public required IFormFile File { get; set; }
}