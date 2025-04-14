using Goognet.Application.DTOs.Requests;
using Goognet.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Goognet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SiteController : ControllerBase
    {
        private readonly ISiteService _siteService;

        public SiteController(ISiteService siteService)
        {
            _siteService = siteService;
        }
        [HttpPost("search")]
        public async Task<IActionResult> Get(SearchSitePagenedRequest request)
        {
            var response = await _siteService.SearchPagenedBy(request);

            return Ok(response);
        }

    }
}
