namespace DanceStudio.Reviews.Application.DTOs
{
    public class ReviewDto
    {
        public string Id { get; set; } 
        public string TargetId { get; set; }
        public string TargetType { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public ReviewerDto Reviewer { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ReviewerDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string? AvatarUrl { get; set; }
    }
}