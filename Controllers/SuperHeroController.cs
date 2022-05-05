using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroeAPI.Models;

namespace SuperHeroeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> heros = new List<SuperHero>
            { new SuperHero{ Id = 1, Name ="Spider Man", FirstName ="Peter", LastName = "Parker", Place ="New York"},
              new SuperHero{ Id = 2, Name ="Batman", FirstName ="Bruce", LastName = "Wayne", Place ="Cd Gotica" }
            };

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllHeroes")]
     /*   public ActionResult<List<SuperHero>> GetSuperHeros()
        {
            return Ok(heros);
        }*/
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeros()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("GetHeroById/{id}")]

      //public ActionResult<SuperHero> GetHeroById(int id)
      public async Task<ActionResult<List<SuperHero>>> GetHeroById(int id)
        {
           // var hero = heros.Find(x => x.Id == id);
           var heroes=await _context.SuperHeroes.FindAsync(id);
            if (heroes == null)
                return BadRequest("hero not found");
            else
                return Ok(heroes);
        }

        [HttpPost("AddHero")]
       // public async Task<ActionResult> AddHero(SuperHero hero)
       public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            //heros.Add(hero);
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]

        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero req)
        {
            //var hero = heros.Find(x => x.Id == req.Id);
            var hero = await _context.SuperHeroes.FindAsync(req.Id);

            if (hero == null)
                return BadRequest("Not found");
            else
            {
                hero.Name = req.Name;
                hero.FirstName = req.FirstName;
                hero.LastName = req.LastName;
                hero.Place = req.Place;
            }
            await _context.SaveChangesAsync();
            //return OK();
            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete("DeleteHero/{id}")]
       // public IActionResult DeleteHero(int id)
       public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            //var hero = heros.Find(x => x.Id == id);
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (id == 0)
                return BadRequest("Id Invalid");
            if (hero == null)
                return NotFound();
            else
            {
                //heros.Remove(hero);
                _context.SuperHeroes.Remove(hero);
                await _context.SaveChangesAsync();
                //return Ok();
                return Ok(await _context.SuperHeroes.ToListAsync());
            }
        }
            


    }
}
