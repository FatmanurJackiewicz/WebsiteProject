using System.Text.Json.Serialization;

namespace AuthAPI.Models;

public class RoleModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    //[JsonIgnore]
    //public virtual ICollection<UserModel> Users { get; set; }

}


