using AutoMapper;
using DanceStudio.Reviews.Application.DTOs;
using DanceStudio.Reviews.Application.Reviews.Commands.CreateReview;
using DanceStudio.Reviews.Domain.Entities;
using DanceStudio.Reviews.Domain.ValueObjects;
using MongoDB.Bson;

namespace DanceStudio.Reviews.Application.Common.Mappings
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {

            CreateMap<ObjectId, string>().ConvertUsing(o => o.ToString());
            CreateMap<string, ObjectId>().ConvertUsing(s => ObjectId.Parse(s));

            CreateMap<Review, ReviewDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id)) 
                .ForMember(d => d.Rating, opt => opt.MapFrom(s => s.Rating.Value));

            CreateMap<Reviewer, ReviewerDto>();

        }
    }
}