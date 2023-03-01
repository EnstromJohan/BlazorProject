using System.ComponentModel.DataAnnotations;

namespace VOD.Common.DTOs
{
    public class FilmDTO
    {
        public int Id { get; set; }
       
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public int DirectorId { get; set; }
        public string? DirectorName { get; set; }
        public bool Free { get; set; }
        
        public string? Description { get; set; }
       
        public string? FilmUrl { get; set; }

        public List<FilmGenreDTO>? FilmGenres { get; set; }

    }

    public class FilmCreateDTO
    {
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public int DirectorId { get; set; }

        public string? DirectorName { get; set; }
        public bool Free { get; set; }

        public string? Description { get; set; }

        public string? FilmUrl { get; set; }
    }

    public class FilmEditDTO : FilmCreateDTO
    {
        public int Id { get; set; }
    }
}
