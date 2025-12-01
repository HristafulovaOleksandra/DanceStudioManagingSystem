using AutoMapper;
using DanceStudio.Reviews.Application.DTOs;
using DanceStudio.Reviews.Domain.Interfaces;
using MediatR;

namespace DanceStudio.Reviews.Application.Reviews.Queries.GetReviewsByTarget
{
    public class GetReviewsByTargetQueryHandler : IRequestHandler<GetReviewsByTargetQuery, IEnumerable<ReviewDto>>
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public GetReviewsByTargetQueryHandler(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> Handle(GetReviewsByTargetQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _repository.GetByTargetAsync(request.TargetId);
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }
    }
}