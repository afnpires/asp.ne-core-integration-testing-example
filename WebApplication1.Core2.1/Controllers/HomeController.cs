using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Core2._1.Models;

namespace WebApplication1.Core2._1.Controllers
{
    public interface IContentService
    {
        string GetAboutContent();
    }

    public class ContentService : IContentService
    {
        public string GetAboutContent()
        {
            return "Your application description page.";
        }
    }

    public class HomeController : Controller
    {
        private readonly IContentService contentService;

        public HomeController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = this.contentService.GetAboutContent();

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
}
