using AutoMapper;
using FarmEase.Domain.DTO;
using FarmEase.Domain.Entities;

namespace FarmEase.Domain.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterModel, ApplicationUser>();
        }
    }
}
