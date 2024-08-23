namespace DataAPI.Models
{
    public class Experiences
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Description { get; set; }

    }
}
