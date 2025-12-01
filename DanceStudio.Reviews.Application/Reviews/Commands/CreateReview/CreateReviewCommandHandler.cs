using DanceStudio.Reviews.Domain.Entities;
using DanceStudio.Reviews.Domain.Interfaces;
using DanceStudio.Reviews.Domain.ValueObjects;
using MediatR;

namespace DanceStudio.Reviews.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, string>
    {
        private readonly IReviewRepository _repository;

        public CreateReviewCommandHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var rating = new Rating(request.Rating);
            var reviewer = new Reviewer(request.UserId, request.UserName, request.AvatarUrl);

            var review = new Review(
                request.TargetId,
                request.TargetType,
                request.Content,
                rating,
                reviewer
            );

            await _repository.AddAsync(review);


            return review.Id.ToString();
        }
    }
}