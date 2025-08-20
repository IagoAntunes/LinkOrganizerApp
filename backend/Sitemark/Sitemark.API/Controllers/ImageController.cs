using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sitemark.Application.Services;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace Sitemark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet("{imageId:guid}")]
        public async Task<IActionResult> GetImage([FromRoute] Guid imageId)
        {
            var imageDto = await imageService.GetImage(imageId);

            if (imageDto == null)
            {
                return NotFound();
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(imageDto.FilePath);
            return File(fileBytes, imageDto.ContentType);
        }
    }
}
