using System.ComponentModel.DataAnnotations.Schema;

namespace VOD.Common.DTOs
{
    public class SimilarFilmsDTO
    {
        public int FilmId { get; set; }
        public int SimilarFilmId { get; set; }

        public virtual FilmDTO Film { get; set; } = null!;
        [ForeignKey("SimilarFilmId")]
        public virtual FilmDTO Similar { get; set; } = null!;
    }

}
