using AutoMapper;
using DanceStudio.Catalog.Bll.DTOs;
using DanceStudio.Catalog.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DanceStudio.Booking.Bll.Profiles
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<Instructor, InstructorDTO>()
                .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Bio))
                .ReverseMap();

            CreateMap<DanceClass, DanceClassDTO>().ReverseMap();

            CreateMap<DanceClassDetail, DanceClassDetailDTO>().ReverseMap();

            CreateMap<InstructorCreateUpdateDTO, Instructor>()
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Biography));
            CreateMap<DanceClassCreateUpdateDTO, DanceClass>();

            CreateMap<DanceClassDetailCreateUpdateDTO, DanceClassDetail>()
                .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src => src.VideoUrl))
                .ForMember(dest => dest.Requirements, opt => opt.MapFrom(src => src.Requirements));
        }
    }
}

