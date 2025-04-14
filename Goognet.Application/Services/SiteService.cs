using Nest;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Goognet.Application.Interfaces;
using Goognet.Application.DTOs.Responses;
using Goognet.Application.DTOs.Requests;
using Goognet.Domain.Entities;

namespace Goognet.Application.Services
{
    public class SiteService: ISiteService
    {
        public readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public SiteService(IElasticClient elasticClient, IMapper mapper, IConfiguration configuration)
        {

            _elasticClient = elasticClient;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<List<SiteResponse>> SearchPagenedBy(SearchSitePagenedRequest request)
        {
           ValidateSearchRequest(request);
            //busca
            var indexSite = _configuration["INDEX_SITE"];

            var result = await _elasticClient.SearchAsync<Site>(s => s
                .Index(indexSite)
                .Size(request.TotalItems)
                .From((request.Page-1) * request.TotalItems)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Title)
                        .Query(request.Title)
                        .Fuzziness(Fuzziness.Auto)  //Tolerância a erros de digitacao
                        .PrefixLength(1)            //
                        .MaxExpansions(10)          //Número máximo de expansoes. Controlar o número de variações de um termo que sao considerados na pesquisa
                        .Operator(Operator.Or)
                    )
                )
                .Sort(sort => sort
                    .Descending("_score")
                )
            );

            if (!result.IsValid)
            {
                throw new Exception("Error during search");
            }

            //mapear os dados: List<Site> -> List<SiteResponse>
            var sitesMapped = _mapper.Map<List<SiteResponse>>(result.Documents);

            // retorna a lista de sites;
            return sitesMapped;
        }
        private static void ValidateSearchRequest(SearchSitePagenedRequest request)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                throw new Exception("Tittle is null or empty");

            }
            if (request.Page<0)
            {
                throw new Exception("The number of pages is negative");

            }
            if (request.TotalItems < 0)
            {
                throw new Exception("The total items is negative");

            }
        }
    }
}
