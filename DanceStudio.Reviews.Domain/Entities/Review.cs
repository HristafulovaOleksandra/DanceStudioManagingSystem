using DanceStudio.Reviews.Domain.Common;
using DanceStudio.Reviews.Domain.Exceptions;
using DanceStudio.Reviews.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DanceStudio.Reviews.Domain.Entities
{
    public class Review : BaseEntity
    {
        [BsonElement("targetId")]
        public string TargetId { get; private set; }

        [BsonElement("targetType")]
        public string TargetType { get; private set; }

        [BsonElement("content")]
        public string Content { get; private set; }

        [BsonElement("rating")]
        public Rating Rating { get; private set; }

        [BsonElement("reviewer")]
        public Reviewer Reviewer { get; private set; }

        [BsonElement("likes")]
        public int Likes { get; private set; }

        [BsonElement("tags")]
        public List<string> Tags { get; private set; }

        public Review(string targetId, string targetType, string content, Rating rating, Reviewer reviewer)
        {
            if (string.IsNullOrWhiteSpace(targetId)) throw new DomainException("Target ID is required.");
            if (string.IsNullOrWhiteSpace(targetType)) throw new DomainException("Target Type is required.");
            if (string.IsNullOrWhiteSpace(content)) throw new DomainException("Review content cannot be empty.");
            if (reviewer == null) throw new DomainException("Reviewer is required."); 

            TargetId = targetId;
            TargetType = targetType;
            Content = content;
            Rating = rating;
            Reviewer = reviewer;
            Likes = 0;
            Tags = new List<string>();
        }

        public void Update(string newContent, Rating newRating)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new DomainException("Review content cannot be empty.");

            Content = newContent;
            Rating = newRating;
            UpdateTimestamp();
        }

        public void AddLike() => Likes++;

        public void RemoveLike()
        {
            if (Likes > 0) Likes--;
        }

        public void AddTag(string tag)
        {
            if (!string.IsNullOrWhiteSpace(tag) && !Tags.Contains(tag))
            {
                Tags.Add(tag);
                UpdateTimestamp();
            }
        }
    }
}