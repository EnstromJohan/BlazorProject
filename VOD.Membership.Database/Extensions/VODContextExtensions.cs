using Microsoft.Identity.Client;
using VOD.Common.DTOs;
using VOD.Membership.Database.Services;

namespace VOD.Membership.Database.Extensions
{
    public static class VODContextExtensions
    {
        public static async Task SeedMembershipData(this IDbService service)
        {

            try
            {
                var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";

                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Name = "Action",
                    Description = description.Substring(20, 50)

                });

                await service.AddAsync<Genre, GenreDTO>(new GenreDTO
                {
                    Name = "Adventure",
                    Description = description.Substring(30, 40)

                });

                await service.SaveChangesAsync();

                var genre1 = await service.SingleAsync<Genre, GenreDTO>(g => g.Name.Equals("Action"));
                var genre2 = await service.SingleAsync<Genre, GenreDTO>(g => g.Name.Equals("Adventure"));

                await service.AddAsync<Film, FilmDTO>(new FilmDTO
                {
                    
                });
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
