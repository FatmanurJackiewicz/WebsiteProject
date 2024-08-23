namespace FileAPI.ViewModel
{
    public class MultipleUploadFileModel
    {
        public IFormFile File { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
    }
}
