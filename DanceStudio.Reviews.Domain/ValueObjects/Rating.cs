using DanceStudio.Reviews.Domain.Common;
using DanceStudio.Reviews.Domain.Exceptions;

namespace DanceStudio.Reviews.Domain.ValueObjects
{
    public record Rating : ValueObject
    {
        public int Value { get; init; }

        public Rating(int value)
        {
            if (value < 1 || value > 5)
            {
                throw new DomainException("Rating must be between 1 and 5 stars.");
            }
            Value = value;
        }

        public static implicit operator Rating(int value) => new(value);

        public static implicit operator int(Rating rating) => rating.Value;
    }
}