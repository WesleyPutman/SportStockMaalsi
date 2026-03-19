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

        [HttpPost("vetement")]
        public async Task<IActionResult> AjouterVetement([FromBody] CreateVetementDto dto){
            var nouveauVetement = new VetementSport(dto.Nom, dto.Prix, dto.Taille, dto.Matiere)
                {
                    CategorieId = dto.CategorieId
                };
            _context.Vetements.Add(nouveauVetement);
            await _context.SaveChangesAsync();
            return Ok(nouveauVetement);
        }
        public class CreateVetementDto{
            public string Nom { get; set; } = string.Empty;
            public decimal Prix { get; set; }
            public string Taille { get; set; } = string.Empty;
            public string Matiere { get; set; } = string.Empty;
            public int CategorieId { get; set; }
        }

        [HttpPut("{id}/reception")]
        public async Task<IActionResult> ReceptionnerStock(int id, [FromBody] int quantiteRecue){
            var article = await _context.Articles.FindAsync(id);
            if(artcile ==null) return NotFound("Article non trouvé");
            
            article.AjouterStock(quantiteRecue);
            await _context.SaveChangesAsync();
            return Ok(new{
                Message = "Stock mis à jour",
                NouveauStock = article.QuantiteEnStock
            });
        }

        [HttpPost("{id}/emplacement")]
        public async Task<IActionResult> AssignerEmplacement(int id, [FromBody] EmplacementDto dto){
            var aticle = await _context.Articles.FindAsync(id);
            if(article == null) return NotFound("Article non trouvé");

            var emplacement = new EmplacementStock{
                Zone = dto.Zone,
                Allee = dto.Allee,
                Rayon = dto.Rayon,
                Niveau = dto.Niveau,
                ArticleSportId = id
            };
            _context.Emplacements.Add(emplacement);
            await _context.SaveChangesAsync();
            return Ok(emplacement);
        }
        public class EmplacementDto{
            public string Zone { get; set; } = string.Empty;
            public string Allee { get; set; } = string.Empty;
            public string Rayon { get; set; } = string.Empty;
            public string Niveau { get; set; } = string.Empty;
        }
    }
}