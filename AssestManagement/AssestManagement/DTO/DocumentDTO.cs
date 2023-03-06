namespace AssestManagement.DTO
{
    public class DocumentDTO
    {
        public int DocumentId { get; set; }
        public int AssetId { get; set; }
        public string DocumentTitle { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string DocumentPath { get; set; }
    }
}
