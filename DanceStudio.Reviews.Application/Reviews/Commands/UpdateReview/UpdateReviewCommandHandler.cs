using DanceStudio.Reviews.Domain.Exceptions;
using DanceStudio.Reviews.Domain.Interfaces;
using DanceStudio.Reviews.Domain.ValueObjects;
using MediatR;
using MongoDB.Bson;

namespace DanceStudio.Reviews.Application.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
    {
        private readonly IReviewRepository _repository;

        public UpdateReviewCommandHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.Id, out var objectId))
            {
                throw new DomainException($"Invalid Review ID: {request.Id}");
            }

            var review = await _repository.GetByIdAsync(objectId);

            if (review == null)
            {
                throw new DomainException($"Review with ID {request.Id} not found.");
            }

            review.Update(request.Content, new Rating(request.Rating));

            await _repository.UpdateAsync(review);
        }
    }
}