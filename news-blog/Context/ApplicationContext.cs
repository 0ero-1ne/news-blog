using Microsoft.EntityFrameworkCore;
using news_blog.Model;

namespace news_blog.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Tag> Tags { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Article> Articles { get; set; }
        
        public DbSet<ArticleTag> ArticleTags { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
    }
}
