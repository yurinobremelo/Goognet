using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goognet.Application.DTOs.Requests
{
    public class SearchSitePagenedRequest
    {
        public string Title { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public int Page { get; set; }

    }
}
