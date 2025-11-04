using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers;

public class GalleryController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;

    public GalleryController(ILogger<HomeController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index(int page = 1)
    {
        var imagesPath = Path.Combine(_env.WebRootPath, "images", "gallery");
        var imageFiles = Directory.GetFiles(imagesPath)
                                .Select(Path.GetFileName)
                                .ToList();

        return View(imageFiles);
    }

    public IActionResult LoadMoreImages(int page)
    {
        var imagesPath = Path.Combine(_env.WebRootPath, "images", "gallery");
        var imageFiles = Directory.GetFiles(imagesPath)
                                .Select(Path.GetFileName)
                                .Skip((page - 1) * 12)
                                .Take(12)
                                .ToList();

        return PartialView("_GalleryImagesPartial", imageFiles);
    }
}