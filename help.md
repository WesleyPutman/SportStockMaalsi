# 📘 Guide de Survie & Protocole de Création d'API .NET

Guide pour m'aider à retenir les commandes/bouts de code afin de refaire sans utiliser de documentation ou d'IA au sein d'un exercice ou même pour l'apprentissage pur.

---

## 🛣️ Le Chemin de l'Information (Architecture)

Pour comprendre comment ça marche, visualise le trajet d'une requête :

1.  **Client (Swagger/Postman)** : Envoie une demande (ex: `GET /api/stock`).
2.  **Controller** (`StockController.cs`) : Le guichetier. Il reçoit la demande.
3.  **DbContext** (`AppDbContext.cs`) : Le cerveau. Il traduit la demande C# en SQL.
4.  **Database** (PostgreSQL/Docker) : Le coffre-fort. Il stocke et rend les données.
5.  **Model** (`ArticleSport.cs`) : Le format. La donnée revient sous forme d'objet C#.
6.  **DTO** (`CreateVetementDto`) : Le colis. On emballe juste ce qu'il faut pour répondre au client.

---

## 🚀 Protocole de Création (Dans l'ordre)

Si tu dois refaire une API demain, suis cet ordre précis :

### 1. Les Fondations (Models)
Toujours commencer par définir "De quoi on parle ?".
*   Créer les classes dans le dossier `Models/`.
*   Utiliser `prop` + Tab pour générer les propriétés.
*   **Astuce** : Pour les relations, définir les FK (`CategorieId`) ET l'objet (`Categorie`).

### 2. Le Cerveau (DbContext)
Une fois les modèles prêts, il faut les déclarer à la base de données.
*   Dans `Data/AppDbContext.cs` : Ajouter un `DbSet<MonModel>` pour chaque table.
*   Surcharger `OnModelCreating` si tu as des relations complexes (Many-to-Many).

### 3. La Configuration (Program.cs et appsettings.json)
Dire à l'application qu'elle doit utiliser ce DbContext et PostgreSQL.
*   `appsettings.json` : Ajouter la `ConnectionStrings`.
*   `Program.cs` : `builder.Services.AddDbContext<AppDbContext>(...)`.

### 4. La Base de Données (Docker & Migrations)
C'est le moment de rendre ça réel.
*   Créer le `docker-compose.yml`.
*   Lancer la DB : `docker-compose up -d`.
*   Faire la migration : `dotnet ef migrations add InitialCreate`.
*   Appliquer : `dotnet ef database update`.

### 5. Les Guichetiers (Controllers)
Enfin, on ouvre l'accès au monde extérieur.
*   Créer `MonController.cs`.
*   Injecter le `AppDbContext` dans le constructeur.
*   Créer les méthodes (`[HttpGet]`, `[HttpPost]`).

---

## ⚡ Bouts de Code "Super Importants" à Retenir

### A. Le Model (Entité)
```csharp
public class Article {
    public int Id { get; set; } // OBLIGATOIRE (Clé primaire auto)
    public string Nom { get; set; } = string.Empty; // Initialiser pour éviter les warnings null

    // Relation One-to-Many (Un article a UNE catégorie)
    public int CategorieId { get; set; } // La clé étrangère
    public Categorie? Categorie { get; set; } // L'objet de navigation
}
```

### B. Le DbContext (Fluent API)
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder){
    // Relation Many-to-Many complexe
    modelBuilder.Entity<Article>()
        .HasMany(a => a.Tags) // a pour Articles
        .WithMany(t => t.Articles) // t pour Tags
        .UsingEntity(j => j.ToTable("ArticleTags")); // Nommer la table de liaison
}
```

### C. Le Controller (Injection & Async)
```csharp
[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase {
    
    private readonly AppDbContext _context; // Champ privé pour stocker l'accès BDD

    // Constructeur : C'est ici qu'on "reçoit" le DbContext (Injection de dépendance)
    public StockController(AppDbContext context){
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(){
        // TOUJOURS utiliser await + ToListAsync() pour ne pas bloquer le serveur
        var data = await _context.Articles
            .Include(a => a.Categorie) // Le JOIN indispensable !
            .ToListAsync();
        return Ok(data);
    }
}
```

### D. Le DTO (Data Transfer Object)
Sert à recevoir des données propres du client (sans ID, sans données techniques).
```csharp
public class CreateArticleDto {
    public string Nom { get; set; } // On demande juste le nom
    public int CategorieId { get; set; } // Et l'ID de la catégorie existante
}
```

---

## 🛠️ Commandes Terminal Vitales

| Action | Commande | context |
| :--- | :--- | :--- |
| **Lancer le projet** | `dotnet watch run` | Dev loop rapide |
| **Créer Migration** | `dotnet ef migrations add NomDeLaModif` | Après modif Model |
| **Appliquer Migration**| `dotnet ef database update` | Pour mettre à jour SQL |
| **Lancer Docker** | `docker-compose up -d` | Démarre la BDD |
| **Arrêter Docker** | `docker-compose down` | Coupe tout |
