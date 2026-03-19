namespace SportStock.Api.Models;

public class Categorie{
    public int Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public ICollection<ArticleSport> Articles { get; set; } = new List<ArticleSport>();
}