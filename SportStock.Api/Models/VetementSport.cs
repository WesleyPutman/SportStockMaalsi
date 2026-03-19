namespace SportStock.Api.Models;

public class VetementSport : ArticleSport{
    public string Taille { get; private set; }
    public string Matiere { get; private set; }
    public string Couleur { get; private set; }
    public string Genre { get; private set; }

    public VetementSport(string nom, decimal prix, string taille, string matiere, string couleur, string genre) : base(nom, prix){
        Taille = taille;
        Matiere = matiere;
        Couleur = couleur;
        Genre = genre;
    }
}