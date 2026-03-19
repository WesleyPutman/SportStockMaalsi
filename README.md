# SportStock API 

API de gestion de stock pour articles de sport, construite avec .NET 8, Entity Framework Core et PostgreSQL (Docker).

## Prérequis

Avant de commencer sur un nouveau PC, assure-toi d'avoir installé :

1.  **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)**
2.  **[Docker Desktop](https://www.docker.com/products/docker-desktop/)** (obligatoire pour la base de données)
3.  **[Git](https://git-scm.com/)**

## Installation Pas à Pas

### 1. Cloner le projet
Ouvre un terminal (PowerShell ou Bash) :
```bash
git clone https://github.com/WesleyPutman/SportStockMaalsi.git
cd SportStockMaalsi
```

### 2. Lancer la Base de Données (via Docker)
Le projet a besoin d'une base PostgreSQL qui tourne. On utilise Docker pour ça.
```bash
cd SportStock.Api
docker-compose up -d
```
*Attendre quelques secondes que le conteneur "db" soit vert dans Docker Desktop.*

### 3. Créer la structure de la base (Migrations)
La base de données est vide au début. Il faut créer les tables.
```bash
# S'assurer d'être toujours dans le dossier SportStock.Api

# Si nécessaire, installer l'outil de migration globalement (une seule fois)
dotnet tool install --global dotnet-ef

# Appliquer les migrations pour créer les tables SQL
dotnet ef database update
```

### 4. Lancer l'API
```bash
dotnet run
```
L'application va démarrer. Regarde dans le terminal pour voir sur quel port elle écoute (ex: `http://localhost:5169`).

## Accès Rapides

*   **Swagger (Documentation API)** : `http://localhost:XXXX/swagger` (Remplace XXXX par le port affiché dans la console)
*   **Voir le stock JSON** : `http://localhost:XXXX/api/stock`

## 🛠 Commandes Utiles pour le développement

*   **Créer une nouvelle migration (après modif des Models) :**
    ```bash
    dotnet ef migrations add NomDeLaModifExplicite
    ```
*   **Mettre à jour la BDD :**
    ```bash
    dotnet ef database update
    ```
*   **Arrêter la base de données :**
    ```bash
    docker-compose down
    ```
