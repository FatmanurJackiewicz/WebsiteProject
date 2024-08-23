using AuthAPI.Models;

namespace DataAPI.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
        public int BlogPostId { get; set; }

        public BlogPosts BlogPost { get; set; }
        public virtual User User { get; set; }
    }
}
