namespace VOD.Membership.Database.Extensions
{
    public static class VODContextExtensions
    {
        public static async Task SeedMembershipData(this IDbService service)
        {

            try
            {
                //var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";

                await service.AddAsync<Director, DirectorDTO>(new DirectorDTO
                {
                    Name = "Richard Schenkman",
                });

                await service.AddAsync<Director, DirectorDTO>(new DirectorDTO
                {
                    Name = "Christopher Nolan",
                });

                await service.SaveChangesAsync();

                var director1 = await service.SingleAsync<Director, DirectorDTO>(d => d.Name.Equals("Richard Schenkman"));
                var director2 = await service.SingleAsync<Director, DirectorDTO>(d => d.Name.Equals("Christopher Nolan"));

                await service.AddAsync<Film, FilmDTO>(new FilmDTO
                {
                    DirectorId = director1.Id,
                    Title = "The man from earth",
                    Description = "asdasldlasd",
                    Free = false,
                    FilmUrl = "adsasdasd"
                }) ;

                await service.AddAsync<Film, FilmDTO>(new FilmDTO
                {
                    DirectorId = director2.Id,
                    Title = "The dark knight",
                    Description = "asdasldlasd",
                    Free = false,
                    FilmUrl = "adsasdasd"
                });

                await service.SaveChangesAsync();

                var film1 = await service.SingleAsync<Film, FilmDTO>(f => f.Title.Equals("The man from earth"));
                var film2 = await service.SingleAsync<Film, FilmDTO>(f => f.Title.Equals("The dark knight"));

                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Id = film1.Id,
                    Name = "Sci-fi",
                    
                    
                });

                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Id = film1.Id,
                    Name = "Action",
                    

                });

                await service.SaveChangesAsync();

                var genre1 = await service.SingleAsync<Genre, GenreDTO>(g => g.Name.Equals("Sci-fi"));
                var genre2 = await service.SingleAsync<Genre, GenreDTO>(g => g.Name.Equals("Action"));
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
