using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/cities")]
    public class CitiesControllers : ControllerBase
    {
        private readonly DataContext _context;

        public CitiesControllers(DataContext context) 
        {
            _context = context;   
        }


        [HttpGet]
        public async Task<IActionResult> GetAstnc()
        {
            return Ok(await _context.Cities.ToListAsync());
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAstnc(int id)
        {
            var cities = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (cities == null)
            {
                return NotFound();
            }
            return Ok(cities);
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(City city)
        {
            try
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(City city)
        {
            try
            {
                _context.Update(city);
                await _context.SaveChangesAsync();
                return Ok(city);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAstnc(int id)
        {
            var cities = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (cities == null)
            {
                return NotFound();
            }

            _context.Remove(cities);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}