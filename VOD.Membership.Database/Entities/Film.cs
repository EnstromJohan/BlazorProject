namespace VOD.Membership.Database.Entities
{
    public class Film : IEntity
    {

        public Film()
        {
            SimilarFilms = new HashSet<SimilarFilms>();
            Genres = new HashSet<Genre>();
        }
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public int DirectorId { get; set; }
        public bool Free { get; set; }
        [MaxLength(200), Required]
        public string? Description { get; set; }
        [MaxLength(1024), Required]
        public string? FilmUrl { get; set; }

        public int GenreId { get; set; }

        public virtual Director? Director { get; set; }
        public virtual ICollection<SimilarFilms> SimilarFilms { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
       
    }

}
