using DanceStudio.Reviews.Domain.Common;
using DanceStudio.Reviews.Domain.Exceptions;

namespace DanceStudio.Reviews.Domain.ValueObjects
{
    public record Reviewer : ValueObject
    {
        public string UserId { get; init; }
        public string FullName { get; init; }
        public string? AvatarUrl { get; init; } 

        public Reviewer(string userId, string fullName, string? avatarUrl = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new DomainException("Reviewer User ID is required.");

            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Reviewer Name is required.");

            UserId = userId;
            FullName = fullName;
            AvatarUrl = avatarUrl;
        }
    }
}