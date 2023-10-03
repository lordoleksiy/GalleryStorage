namespace GalleryStorage.Models
{
    public class StorageModel
    {
        public string Name { get; set; }
        public Uri Url { get; set; }
        public IList<PhotoModel> PhotoModels { get; set; }
        public IFormFile NewPhoto { get; set; }
        public StorageModel() 
        {
            Name = string.Empty;
            PhotoModels = new List<PhotoModel>();
        }
        public StorageModel(string name) 
        {
            Name = name;
            PhotoModels = new List<PhotoModel>();
        }   
        public StorageModel(string name, Uri url, IList<PhotoModel> photoModels) 
        {
            Name = name;
            Url = url;
            PhotoModels = photoModels; 
        }
    }
}
