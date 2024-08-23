namespace DataAPI.Models
{
    public class Educations
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string School { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }


    }
}
