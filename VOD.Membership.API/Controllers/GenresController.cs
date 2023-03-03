using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.Membership.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        private readonly IDbService _db;

        public GenresController (IDbService db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {

            try
            {
                var genres = await _db.GetAsync<Genre, GenreDTO>();
                return Results.Ok(genres);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            try
            {
                _db.Include<Genre>();

                var genre = await _db.SingleAsync<Genre, GenreDTO>(g => g.Id == id);
                return Results.Ok(genre);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IResult> Post([FromBody] GenreCreateDTO dto)
        {

            try
            {
                var genre = await _db.AddAsync<Genre, GenreCreateDTO>(dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();
                return Results.Created(_db.GetURI(genre), genre);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] GenreEditDTO dto)
        {

            try
            {
                if (id != dto.Id) return Results.BadRequest($"Wrong id. URI Id: {id} DTO Id: {dto.Id}");
                var exists = await _db.AnyAsync<Film>(g => g.Id == dto.Id);
                if (!exists) return Results.NotFound("Director not found");

                exists = await _db.AnyAsync<Genre>(g => g.Id == id);
                if (!exists) return Results.NotFound("Director not found");



                _db.Update<Genre, GenreEditDTO>(id, dto);
                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var exists = await _db.AnyAsync<Genre>(g => g.Id == id);
                if (!exists) return Results.NotFound("Film not found.");

                var success = await _db.DeleteAsync<Genre>(id);
                if (!success) return Results.NotFound("Film not found.");

                var result = await _db.SaveChangesAsync();
                if (!result) return Results.BadRequest();

                return Results.NoContent();
            }

            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
