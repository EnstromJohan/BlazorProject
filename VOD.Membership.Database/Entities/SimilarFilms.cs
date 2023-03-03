using System.ComponentModel.DataAnnotations.Schema;

namespace VOD.Membership.Database.Entities
{
    public class SimilarFilms
    {
        public int Id { get; set; }
        public int SimilarFilmId { get; set; }

        public virtual Film Film { get; set; } = null!;
        [ForeignKey("SimilarFilmId")]
        public virtual Film Similar { get; set; } = null!;

    }
}
