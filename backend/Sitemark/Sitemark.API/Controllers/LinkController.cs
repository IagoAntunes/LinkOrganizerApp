using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sitemark.API.Dtos.Requests;
using Sitemark.Application.Services;
using Sitemark.Domain.Dtos;
using System.Security.Claims;

namespace Sitemark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService linkService;
        private readonly IMapper mapper;

        public LinkController(
            ILinkService linkService,
            IMapper mapper
            )
        {
            this.linkService = linkService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var getLinksResult = await linkService.GetLinksAsync(userId);
            if (getLinksResult.IsSuccess)
            {
                return Ok(getLinksResult.Value);
            }
            else
            {
                return BadRequest(getLinksResult.Error);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LinkCreateRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var linkCreateDto = mapper.Map<LinkCreateDto>(request);
            var createLinkResult = await linkService.CreateLinkAsync(linkCreateDto, userId);

            if (createLinkResult.IsSuccess)
            {  
                return CreatedAtAction(nameof(GetAll), new { id = createLinkResult.Value.Id }, createLinkResult.Value);
            }
            else
            {
                return BadRequest(createLinkResult.Error);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute ] Guid id)
        {
            var deleteLinkResult = await linkService.DeleteLinkAsync(id);
            if (deleteLinkResult.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(deleteLinkResult.Error);
            }
        }

    }
}
