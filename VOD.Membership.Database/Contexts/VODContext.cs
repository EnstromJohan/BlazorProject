using Microsoft.EntityFrameworkCore;
using VOD.Membership.Database.Entities;

namespace VOD.Membership.Database.Contexts
{
    public class VODContext : DbContext
    {
        public DbSet<Director> Directors => Set<Director>();
        public DbSet<Film> Films => Set<Film>();
        public DbSet<FilmGenre> FilmGenres => Set<FilmGenre>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<SimilarFilms> SimilarFilm => Set<SimilarFilms>();

        public VODContext(DbContextOptions<VODContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e
            => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<SimilarFilms>().HasKey(ci => new { ci.Id, ci.SimilarFilmId });
            builder.Entity<FilmGenre>().HasKey(ci => new { ci.FilmId, ci.GenreId });


            builder.Entity<Film>(entity =>
            {
                entity
                    .HasMany(d => d.SimilarFilms)
                    .WithOne(p => p.Film)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull);


                entity.HasMany(d => d.Genres)
                      .WithMany(p => p.Films)
                      .UsingEntity<FilmGenre>()
                      .ToTable("FilmGenres");
            });


        }

        
    }

    

   
}
