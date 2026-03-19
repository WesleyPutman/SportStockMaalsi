using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportStock.Api.Data;
using SportStock.Api.Models;

namespace SportStock.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly AppDbContext _context;

    public StockController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetStockTotal()
    {
        var articles = await _context.Articles
            .Include(a => a.Categorie)
            .Include(a => a.EmplacementStock)
            .Include(a => a.Fournisseurs)
            .ToListAsync();
        return Ok(articles);
    }

    [HttpPost("categorie/{nom}")]
    public async Task<IActionResult> CreerCategorie(string nom){
        var categorie = new Categorie { Nom = nom };
        _context.Categories.Add(categorie);
        await _context.SaveChangesAsync();
        return Ok(categorie);
    }


    [HttpPost("vetement")]
    public async Task<IActionResult> AjouterVetement([FromBody] CreateVetementDto dto)
    {
        var nouveauVetement = new VetementSport(dto.Nom, dto.Prix, dto.Taille, dto.Matiere, dto.Couleur, dto.Genre)
        {
            CategorieId = dto.CategorieId
        };
        _context.Vetements.Add(nouveauVetement);
        await _context.SaveChangesAsync();
        return Ok(nouveauVetement);
    }

    public class CreateVetementDto
    {
        public string Nom { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public int CategorieId { get; set; }
        public string Taille { get; set; } = string.Empty;
        public string Matiere { get; set; } = string.Empty;
        public string Couleur { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
    }

    [HttpPut("{id}/reception")]
    public async Task<IActionResult> ReceptionnerStock(int id, [FromBody] int quantiteRecue)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) return NotFound("Article non trouvé");

        article.AjouterStock(quantiteRecue);
        await _context.SaveChangesAsync();
        return Ok(new
        {
            Message = "Stock mis à jour",
            NouveauStock = article.QuantiteEnStock
        });
    }

    [HttpPost("{id}/emplacement")]
    public async Task<IActionResult> AssignerEmplacement(int id, [FromBody] EmplacementDto dto)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article == null) return NotFound("Article non trouvé");

        var emplacement = new EmplacementStock
        {
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

    [HttpPost("fournisseur/{nomSociete}")]
    public async Task<IActionResult> CreerFournisseur(string nomSociete){
        var fournisseur = new Fournisseur { NomSociete = nomSociete };
        _context.Fournisseurs.Add(fournisseur);
        await _context.SaveChangesAsync();
        return Ok(fournisseur);
    }

    [HttpPost("{articleId}/fournisseur/{fournisseurId}")]
    public async Task<IActionResult> LierFournisseur(int articleId, int fournisseurId){
        var article = await _context.Articles
            .Include(a => a.Fournisseurs)
            .FirstOrDefaultAsync(a => a.Id == articleId);

        if (article == null) return NotFound("Article non trouvé");

        var fournisseur = await _context.Fournisseurs.FindAsync(fournisseurId);
        if (fournisseur == null) return NotFound("Fournisseur non trouvé");

        if (!article.Fournisseurs.Any(f => f.Id == fournisseurId))
        {
            article.Fournisseurs.Add(fournisseur);
            await _context.SaveChangesAsync();
        }

        return Ok(new { 
            Message = $"Le fournisseur '{fournisseur.NomSociete}' a bien été lié à l'article '{article.Nom}'" 
        });
    }

    public class EmplacementDto
    {
        public string Zone { get; set; } = string.Empty;
        public string Allee { get; set; } = string.Empty;
        public string Rayon { get; set; } = string.Empty;
        public string Niveau { get; set; } = string.Empty;
    }
}