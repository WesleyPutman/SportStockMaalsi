# Étape 1 : Build de l'application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie du fichier projet et restauration des dépendances
COPY ["SportStock.Api.csproj", "./"]
RUN dotnet restore "SportStock.Api.csproj"

# Copie du reste du code et compilation
COPY . .
RUN dotnet publish "SportStock.Api.csproj" -c Release -o /app/publish

# Étape 2 : Image finale (allégée pour la production)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SportStock.Api.dll"]