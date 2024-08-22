using System.Text.Json.Serialization;

namespace AuthAPI.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    //[JsonIgnore]
    //public virtual ICollection<UserModel> Users { get; set; }

}


