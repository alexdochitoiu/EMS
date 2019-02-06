namespace WebAPI.Models.UploadModel
{
    public class ImageModel
    {
        public string Base64String { get; set; }
    }

    public class CreatingUploadModel
    {
        public string IncidentId { get; set; }
        public ImageModel[] Images { get; set; }
    }
}
