using DataAPI.ModelConfigurations;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
   : base(options) { }

        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<ContactMessages> ContactMessages { get; set; }
        public DbSet<Educations> Educations { get; set; }
        public DbSet<Experiences> Experiences { get; set; }
        public DbSet<Projects> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AboutMeConfigurations());

            modelBuilder.ApplyConfiguration(new ContactMessagesConfigurations());

            modelBuilder.ApplyConfiguration(new EducationsConfigurations());

            modelBuilder.ApplyConfiguration(new ExperiencesConfigurations());

            modelBuilder.ApplyConfiguration(new PersonalInfoConfigurations());

            modelBuilder.ApplyConfiguration(new ProjectsConfigurations());
            base.OnModelCreating(modelBuilder);


        }
    }
}
