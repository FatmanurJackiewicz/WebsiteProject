using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace AuthAPI.AppDbContext;

public partial class AppDbContext:DbContext
{

    public AppDbContext (DbContextOptions <AppDbContext> options)
        :base(options)
    {

    }

    public virtual DbSet<UserModel> UserModel { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.HasKey(k => k.Id);
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
