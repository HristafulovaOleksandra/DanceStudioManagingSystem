using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DanceStudio.Reviews.Domain.Common
{
    public abstract class BaseEntity
    {
        [BsonId]
        public ObjectId Id { get; protected set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; protected set; }

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            Id = ObjectId.GenerateNewId();
            CreatedAt = DateTime.UtcNow;
        }

        protected void UpdateTimestamp()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}