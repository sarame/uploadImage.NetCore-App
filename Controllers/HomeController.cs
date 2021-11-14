using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;


namespace uploadImage.NetCore.Controllers
{
    public class HomeController : Controller
    {

        private IHostingEnvironment hostingEnv;

        public HomeController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Message"] = "Upload Image Sara .";
            ViewData["test"]="";

            return View();
        }

                    /* save in temp */

        //  [HttpPost]
        // public IActionResult Add(List<IFormFile> files)
        // {
        //     long size = files.Sum(f => f.Length);
        //     full path to file in temp location
        //     var filePath = Path.GetTempFileName();

        //     foreach (var formFile in files)
        //     {
        //         if (formFile.Length > 0)
        //         {
        //             using (var stream = new FileStream(filePath, FileMode.Create))
        //             {
        //                  formFile.CopyToAsync(stream);
        //             }
        //         }
        //     }

        //     process uploaded files
        //     Don't rely on or trust the FileName property without validation.

        //     return Ok(new { count = files.Count, size, filePath});
        // }


        [HttpPost]
        public async Task<IActionResult> Add(IFormFile files)
        {
            var FName = files.FileName;
            var fileExt = Path.GetExtension(files.FileName);

            var imagePath = Path.Combine(hostingEnv.WebRootPath, "Images");

            if ( fileExt.ToLower().EndsWith(".png") || fileExt.ToLower().EndsWith(".jpg"))
            {

                using (var fileStream = new FileStream(Path.Combine(imagePath,FName), FileMode.Create))
                {
                      await files.CopyToAsync(fileStream);
                }
                ViewData["test"] = "/images/" + FName;
                return await Task.FromResult(View());

            }
            else
            {
                return await Task.FromResult(Error());
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
