namespace AssestManagement.DTO
{
    public class AssetImageDTO
    {
        public int AssetImageId { get; set; }
        public int? AssetId { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

    }
}
