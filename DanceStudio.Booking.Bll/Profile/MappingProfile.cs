using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using DanceStudio.Booking.Bll.DTOs;
using DanceStudio.Booking.Domain.Entities;
using BookingEntity = DanceStudio.Booking.Domain.Entities.Booking;

namespace DanceStudio.Booking.Bll.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<BookingEntity, BookingDetailsDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MapStatusToString(src.Status)));

            CreateMap<BookingItem, BookingItemDto>();

            CreateMap<ClientBookingSummary, BookingSummaryDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => MapStatusToString(src.Status)));

            
            CreateMap<Client, ClientDto>();

            CreateMap<CreateClientDto, Client>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<UpdateClientDto, Client>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
        }

        private static string MapStatusToString(short status)
        {
            return status switch
            {
                0 => "Pending",
                1 => "Confirmed",
                2 => "Cancelled",
                _ => "Unknown"
            };
        }
    }
}
