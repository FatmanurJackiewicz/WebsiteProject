using AuthAPI.Models;

namespace DataAPI.Models;

public class Educations
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Degree { get; set; }
    public string School { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public virtual User User { get; set; }
}

