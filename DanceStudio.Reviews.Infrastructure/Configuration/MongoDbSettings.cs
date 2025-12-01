namespace DanceStudio.Reviews.Infrastructure.Configuration
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ReviewsCollectionName { get; set; } = "reviews";
    }
}