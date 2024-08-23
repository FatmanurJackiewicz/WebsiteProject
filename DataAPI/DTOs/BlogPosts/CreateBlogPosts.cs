namespace DataAPI.DTOs.BlogPosts
{
    public class CreateBlogPosts
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Context { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
    }
}
