namespace VOD.Common.DTOs
{
    public class SimilarFilmsDTO
    {
        public int Id { get; set; }
        public int ParentFilmId { get; set; }
        public int SimilarFilmId { get; set; }
        public FilmDTO? Film { get; set; }
    }

}
