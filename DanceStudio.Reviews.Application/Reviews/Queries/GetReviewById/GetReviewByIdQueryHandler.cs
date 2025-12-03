using AutoMapper;
using DanceStudio.Reviews.Application.DTOs;
using DanceStudio.Reviews.Domain.Entities;
using DanceStudio.Reviews.Domain.Exceptions;
using DanceStudio.Reviews.Domain.Interfaces;
using MediatR;
using MongoDB.Bson;

namespace DanceStudio.Reviews.Application.Reviews.Queries.GetReviewById
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDto>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public GetReviewByIdQueryHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ReviewDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.Id, out var objectId))
            {
                throw new DomainException("Invalid ID format");
            }

            var review = await _repository.GetByIdAsync(objectId);

            if (review == null)
            {
                throw new NotFoundException(nameof(Review), request.Id);
            }

            return _mapper.Map<ReviewDto>(review);
        }
    }
}