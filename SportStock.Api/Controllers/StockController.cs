using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportStock.Api.Data;

namespace SportStock.Api.Controllers;
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase{
        private readonly AppDbContext _context;

        public StockController(AppDbContext context){
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetStockTotal(){
            var articles = await _context.Articles
                .Include(a => a.Categorie)
                .Include(a => a.Emplacement)
                .Include(a => a.Fournisseurs)
                .ToListAsync();
            return Ok(articles);
        }
    }
}