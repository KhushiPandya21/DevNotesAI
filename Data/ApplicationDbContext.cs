using DevNotesAI.Models;
using Microsoft.EntityFrameworkCore;

namespace DevNotesAI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<SourceFile> SourceFiles { get; set; }

        public DbSet<CodeAnalysis> CodeAnalyses { get; set; }

        public DbSet<AIAnalysis> AIAnalyses { get; set; }
        public DbSet<AnalysisHistory> AnalysisHistories { get; set; }

        public DbSet<Documentation> Documentations { get; set; }

        public DbSet<SearchHistory> SearchHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SearchHistory>()
                .HasOne(s => s.User)
                .WithMany(u => u.SearchHistories)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}