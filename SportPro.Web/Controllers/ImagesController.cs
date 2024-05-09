using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Repositories;
using System.Net;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
public class ImagesController : Controller
{
    private readonly IImagesRepository imagesRepository;

    public ImagesController(IImagesRepository imagesRepository)
    {
        this.imagesRepository = imagesRepository;
    }

    [HttpPost]
    public async Task<IActionResult> UploadAsync(IEnumerable<IFormFile> files)
    {
        var images = await imagesRepository.UploadAsync(files);

        if (images == null)
        {
            return Problem("Error uploading images", null, (int)HttpStatusCode.InternalServerError);
        }

        return new JsonResult(new { link = images });
    }


    //[HttpGet]
    //public async Task<IActionResult> GetImagesAsync(int id)
    //{
    //    var images = await imagesRepository.GetImagesAsync(id);
    //    return Ok(images);
    //}

    //[HttpDelete]
    //public async Task<IActionResult> DeleteImagesAsync(IEnumerable<string> imageUrls)
    //{
    //    var images = await imagesRepository.DeleteImagesAsync(imageUrls);
    //    //return Ok(images);
    //    return RedirectToAction("Index", "Promocije");
    //}


}
