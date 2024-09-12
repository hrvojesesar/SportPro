using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportPro.Web.Interfaces;
using SportPro.Web.Models.Domains;
using SportPro.Web.Repositories;
using System.Net;

namespace SportPro.Web.Controllers;

[Route("[controller]/[action]")]
[ApiController]
[Authorize]
public class ImagesController : Controller
{
    private readonly IImagesRepository imagesRepository;

    public ImagesController(IImagesRepository imagesRepository)
    {
        this.imagesRepository = imagesRepository;
    }

    /// <summary>
    /// Spremanje slike u CDN
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public async Task<IActionResult> UploadAsync(IEnumerable<IFormFile> files)
    {
        var images = await imagesRepository.UploadAsync(files);

        if (images == null)
        {
            return Problem("Error uploading images", null, (int)HttpStatusCode.InternalServerError);
        }

        if (Request.Headers["Accept"] == "application/json")
        {
            return Ok("Slika je uspješno spremljena u CDN!");
        }

        return new JsonResult(new { link = images });
    }
}
