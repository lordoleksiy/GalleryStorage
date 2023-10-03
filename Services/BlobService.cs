using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using GalleryStorage.Models;
using System.Security.Policy;

namespace GalleryStorage.Services
{
    public class BlobService
    {
        private readonly BlobServiceClient _blobClient;
        public BlobService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }
        public async Task<IEnumerable<string>> GetContainersAsync()
        {
            var resultSegment = _blobClient.GetBlobContainersAsync();
            var response = new List<string>();
            await foreach (var container in resultSegment) 
            {
                response.Add(container.Name);
            }
            return response;
        }

        public async Task<StorageModel> GetPhotosByContainerName(string name)
        {
            var containerClient = _blobClient.GetBlobContainerClient(name);
            var response = new StorageModel(containerClient.Name);
            var blobs = containerClient.GetBlobsAsync();
            await foreach (var blob in blobs) 
            {
                
                var blobUri = new Uri($"{containerClient.Uri.AbsoluteUri}/{blob.Name}");
                response.PhotoModels.Add(new PhotoModel(blob.Name, blobUri));
            }
            return response;
        }

        public async Task<bool> CreateStorage(StorageListModel model)
        {
            var containerClient = _blobClient.GetBlobContainerClient(model.NewContainerName);
            await containerClient.CreateAsync(PublicAccessType.BlobContainer);
            model.Names.Append(model.NewContainerName);
            return true;
        }

        public async Task<Uri> UploadImage(string name, IFormFile imageFile)
        {
            var container = _blobClient.GetBlobContainerClient(name);
            var blob = container.GetBlobClient(imageFile.FileName);
            await blob.UploadAsync(imageFile.OpenReadStream());
            
            return blob.Uri;
        }

        public async Task<bool> DeleteImage(string storageName, string photoName)
        {
            var container = _blobClient.GetBlobContainerClient(storageName);
            var blob = container.GetBlobClient(photoName);
            return await blob.DeleteIfExistsAsync();
        }
        public async Task<bool> DeleteStorage(string name)
        {
            var container = _blobClient.GetBlobContainerClient(name);
            return await container.DeleteIfExistsAsync();
        }
    }
}
