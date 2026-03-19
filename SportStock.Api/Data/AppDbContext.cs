using Microsoft.EntityFrameworkCore;
using SportStock.Api.Models;

namespace SportStock.Api.Data{
    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ArticleSport> Articles { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<EmplacementStock> Emplacements { get; set; }
        public DbSet<VetementSport> Vetements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<ArticleSport>()
                .HasOne(a => a.EmplacementStock)
                .WithOne(e => e.Article)
                .HasForeignKey<EmplacementStock>(e => e.ArticleSportId);
            modelBuilder.Entity<ArticleSport>()
                .HasMany(a => a.Fournisseurs)
                .WithMany(f => f.ArticlesFournis)
                .UsingEntity(j => j.ToTable("ArticleFournisseur"));
        }
    }
}