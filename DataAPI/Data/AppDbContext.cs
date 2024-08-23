using DataAPI.ModelConfigurations;
using DataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Data
{
    public class AppDbContext :DbContext
    {
        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<BlogPosts> BlogPosts { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<ContactMessages> ContactMessages { get; set; }
        public DbSet<Educations> Educations { get; set; }
        public DbSet<Experiences> Experiences { get; set; }
        public DbSet<PersonalInfo> PersonalInfo { get; set; }
        public DbSet<Projects> Projects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AboutMeConfigurations());
            modelBuilder.ApplyConfiguration(new BlogPostsConfigurations());
            modelBuilder.ApplyConfiguration(new CommentsConfigurations());
            modelBuilder.ApplyConfiguration(new ContactMessagesConfigurations());
            modelBuilder.ApplyConfiguration(new EducationsConfigurations());
            modelBuilder.ApplyConfiguration(new ExperiencesConfigurations());
            modelBuilder.ApplyConfiguration(new PersonalInfoConfigurations());
            modelBuilder.ApplyConfiguration(new ProjectsConfigurations());

            base.OnModelCreating(modelBuilder);


        }
    }
}
