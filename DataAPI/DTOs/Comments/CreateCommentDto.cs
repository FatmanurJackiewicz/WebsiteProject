namespace DataAPI.DTOs.Comments
{
    public class CreateCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }
        public byte IsApproved { get; set; }
        public int BlogPostId { get; set; }
        public int UserId { get; set; }
    }
}
