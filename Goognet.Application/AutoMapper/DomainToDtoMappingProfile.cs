using AutoMapper;
using Goognet.Application.DTOs.Responses;
using Goognet.Domain.Entities;


namespace Goognet.Application.AutoMapper
{
    public class DomainToDtoMappingProfile: Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Site, SiteResponse>();
        }
    }
}
