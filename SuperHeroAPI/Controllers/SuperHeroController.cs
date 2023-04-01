using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddHero(SuperHero superHero)
        {
            await context.SuperHeroes.AddAsync(superHero);
            context.SaveChanges();
            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            else
                return Ok(hero);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHero(SuperHero superhero)
        {
            var hero = await context.SuperHeroes.FindAsync(superhero.Id);
            if (hero == null)
                return BadRequest("Hero not found");
            
            hero.Name = superhero.Name;
            hero.FirstName = superhero.FirstName;
            hero.LastName = superhero.LastName;
            hero.Place = superhero.Place;
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");

            context.SuperHeroes.Remove(hero);
            await context.SaveChangesAsync();
            return Ok($"{hero.Name} deleted");
        }
    }
}
