namespace VOD.Membership.Database.Entities
{
    public class SimilarFilms
    {
        public int Id { get; set; }
        public int ParentFilmId { get; set; }
        public int SimilarFilmId { get; set; }
        public virtual ICollection<Film>? Film { get; set; }
    }
}
