using DanceStudio.Reviews.Domain.Exceptions;
using DanceStudio.Reviews.Domain.Interfaces;
using MediatR;
using MongoDB.Bson;

namespace DanceStudio.Reviews.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
    {
        private readonly IReviewRepository _repository;

        public DeleteReviewCommandHandler(IReviewRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            if (!ObjectId.TryParse(request.Id, out var objectId))
            {
                throw new DomainException($"Invalid Review ID: {request.Id}");
            }

            await _repository.DeleteAsync(objectId);
        }
    }
}