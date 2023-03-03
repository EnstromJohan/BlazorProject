using System.Reflection.PortableExecutable;

namespace VOD.Common.DTOs
{
    public class FilmGenreDTO
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int GenreId { get; set; }  
    }

    public class FilmGenreCreateDTO
    {
        public int FilmId { get; set; }
        public int GenreId { get; set; }
    }

    public class FilmGenreEditDTO : FilmGenreCreateDTO
    {
        public int Id { get; set; }
    }
}