using Azure.Storage.Blobs;
using GalleryStorage.Models;
using GalleryStorage.Services;
using Microsoft.AspNetCore.Mvc;

namespace GalleryStorage.Controllers
{
    public class StorageController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlobService _service;

        public StorageController(ILogger<HomeController> logger, BlobService service)
        {
            _logger = logger;
            _service = service;
        }
        public async Task<ActionResult> Index(StorageModel model)
        {
            if (model.Name == null || !model.Name.Any())
            {
                return RedirectToAction("Index", "Home");
            }
            var storage = await _service.GetPhotosByContainerName(model.Name);
            return View(storage);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(StorageModel model)
        {
            await _service.UploadImage(model.Name, model.NewPhoto);
            return RedirectToAction("Index", "Storage", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string storageName)
        {
            await _service.DeleteStorage(storageName);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(string photoName, string storageName)
        {
            await _service.DeleteImage(storageName, photoName);
            return RedirectToAction("Index", "Storage", new StorageModel(storageName));
        }
    }
}
