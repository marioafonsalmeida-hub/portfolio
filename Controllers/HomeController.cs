using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        string imagePath = Path.Combine(_env.WebRootPath, "images", "home");
        List<string> paths = new List<string>();

        try
        {
            if (IsPhone(Request))
            {
                paths.AddRange(GetImagePaths(imagePath, "phone"));
            }
            else
            {
                paths.AddRange(GetImagePaths(imagePath, "computer"));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving images.");
        }

        return View(paths);
    }

    private bool IsPhone(HttpRequest request)
    {
        var userAgent = request.Headers["User-Agent"].ToString().ToLower();
        return userAgent.Contains("iphone") || userAgent.Contains("android");
    }

    private IEnumerable<string> GetImagePaths(string basePath, string deviceType)
    {
        string devicePath = Path.Combine(basePath, deviceType);
        var images = Directory.GetFiles(devicePath)
            .Select(Path.GetFileName)
            .ToList();
        return images.Select(image => Path.Combine("home", deviceType, image!));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}