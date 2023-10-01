using Microsoft.EntityFrameworkCore;
using Logic.Models;

namespace Logic.DB
{
    public class LogicContext : DbContext
    {
        public LogicContext(DbContextOptions<LogicContext> options)
        : base(options)
        { 
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Answer>().ToTable("Answer");
            modelBuilder.Entity<Tag>().ToTable("Tag");
        }
    }
}
