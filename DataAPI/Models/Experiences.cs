using AuthAPI.Models;

namespace DataAPI.Models
{
    public class Experiences
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}
