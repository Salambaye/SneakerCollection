# --- Étape 1 : build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copie uniquement le .csproj d'abord pour profiter du cache Docker sur les dépendances
COPY *.csproj .
RUN dotnet restore

# Copie le reste du code source et compile en mode Release
COPY . .
RUN dotnet publish -c Release -o /app

# --- Étape 2 : runtime (image plus légère) ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Render fournit le port d'écoute via la variable d'environnement PORT
ENV ASPNETCORE_URLS=http://+:$PORT

# Désactive le rechargement automatique de la config (FileSystemWatcher / inotify),
# qui plante sur Render à cause du quota limité de file descriptors du conteneur
ENV DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false

ENTRYPOINT ["dotnet", "SneakerCollection.dll"]
