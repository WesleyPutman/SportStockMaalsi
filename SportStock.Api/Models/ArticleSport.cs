namespace SportStock.Api.Models;

public abstract class ArticleSport{
    public int Id { get; protected set; }
    public string Nom { get; protected set; }
    public decimal Prix { get; protected set; }
    public int QuantiteEnStock { get; protected set; }

    //ManytoOne Une catégorie peut avoir plusieurs articles
    public int CategorieId { get; set; }
    public Categorie? Categorie { get; protected set; }

    //OneToOne Un article a un emplacement de stock
    public EmplacementStock? EmplacementStock { get; protected set; }

    //ManytoMany Un article peut être fourni par plusieurs fournisseurs et un fournisseur peut fournir plusieurs articles
    public ICollection<Fournisseur> Fournisseurs { get; protected set; } = new List<Fournisseur>();

    protected ArticleSport(string nom, decimal prix){
        Nom = nom;
        Prix = prix;
        QuantiteEnStock = 0;
    }

    public void AjouterStock(int quantite){
        if(quantite > 0) QuantiteEnStock += quantite;
    }
}