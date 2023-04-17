using CatalogBackend.Context;
using CatalogBackend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CatalogBackend.Controllers;

[Controller]
[Route("/orar/v1")]
[ApiController]
public class OrarController : Controller
{
    private readonly MyDbContext _context;

    //Get controller for all entities from database 
    public OrarController(MyDbContext context)
    {
        _context = context;
    }

    //Http Controller -> return all rows in database
    [HttpGet]
    [Route("/all")]
    public async Task<ActionResult<IEnumerable<Orar>>> GetAll()
    {
        if (_context.Orars == null) return NotFound();

        return await _context.Orars.ToListAsync();
    }

    [HttpGet("{grp}")]
    public async Task<ActionResult<IEnumerable<Orar>>> GetOrar(string grp)
    {
        if (_context.Orars == null) return NotFound();

        var orar = await _context.Orars.Where(e => e.Grp == grp).ToListAsync();

        if (orar.IsNullOrEmpty()) return NotFound();
        return orar;
    }
    
    //Http post requests: 
    [HttpPost]
    public async Task<ActionResult<Orar>> PostOrar(Orar orar)
    {
        if (_context.Orars == null) return Problem("Nu ati introdus nimic. Va rugam incercati din nou.");

        _context.Orars.Add(orar);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetOrar", new { grp = orar.Grp }, orar);
    }

    //HttpPut request 
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrar(int id, Orar orar)
    {
        if (id != orar.Id) return BadRequest();

        _context.Entry(orar).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrarExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrar(long id)
    {
        if (_context.Orars == null) return NotFound();

        var orar = await _context.Orars.FindAsync(id);
        if (orar == null) return NotFound();

        _context.Orars.Remove(orar);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OrarExists(int id)
    {
        return (_context.Orars?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}