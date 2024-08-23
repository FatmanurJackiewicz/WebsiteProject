namespace DataAPI.DTOs.Comments
{
    public class UpdateCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public byte IsApproved { get; set; }
        public int BlogPostId { get; set; }
        public int UserId { get; set; }
    }
}
