namespace SportStock.Api.Models;

public class Fournisseur{
    public int Id { get; set; }
    public string NomSociete { get; set; } = string.Empty;
    public ICollection<ArticleSport> ArticlesFournis { get; set; } = new List<ArticleSport>();
}