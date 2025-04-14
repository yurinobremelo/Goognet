using Goognet.Application.DTOs.Requests;
using Goognet.Application.DTOs.Responses;

namespace Goognet.Application.Interfaces
{
    public interface ISiteService
    {
        public Task<List<SiteResponse>> SearchPagenedBy(SearchSitePagenedRequest request);
    }
}
