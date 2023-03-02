// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VOD.Membership.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly IDbService _db;

        public SeedController(IDbService db)
        {
            _db = db;
        }
       
        // POST api/<SeedController>
        [HttpPost]
        public async Task <IResult> Post()
        {

            try
            {
                await _db.SeedMembershipData();
                return Results.NoContent();
            }
            catch (Exception)
            {
                return Results.BadRequest();
            }
        }

    }
}
