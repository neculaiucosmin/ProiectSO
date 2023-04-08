using CatalogBackend.Context;
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
    
    public OrarController(MyDbContext context)
    {
        _context = context;
    }
    //Http Controller -> return all rows in database
    [HttpGet]
    [Route("/all")]
    public async Task<ActionResult<IEnumerable<Entities.Orar>>> getAll()
    {
        if (_context.Orars == null)
        {
            return NotFound();
        }
    
        return await _context.Orars.ToListAsync();
    }

    [HttpGet("{grp}")]
    public async Task<ActionResult<IEnumerable<Entities.Orar>>> GetOrar(string grp)
    {
        if (_context.Orars == null)
        {
            return NotFound();
        }

        var orar = await _context.Orars.Where(e => e.Grp == grp).ToListAsync();

        if (orar.IsNullOrEmpty())
        {
            return NotFound();
        }

        return orar;

    }
    
    
}