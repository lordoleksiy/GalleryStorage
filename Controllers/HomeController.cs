using GalleryStorage.Models;
using GalleryStorage.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GalleryStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlobService _service;

        public HomeController(ILogger<HomeController> logger, BlobService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var containerList = new StorageListModel(await _service.GetContainersAsync());
            return View(containerList);
        }

        public async Task<IActionResult> Create(StorageListModel model)
        {
            model.Names = await _service.GetContainersAsync();
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            if (!model.IsCorrectNewName())
            {
                ModelState.AddModelError(nameof(model.NewContainerName), "Таке ім'я вже існує");
                return View("Index", model);
            }
            await _service.CreateStorage(model);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string error)
        {
            return View(JsonConvert.DeserializeObject<ErrorViewModel>(error));
        }
    }
}