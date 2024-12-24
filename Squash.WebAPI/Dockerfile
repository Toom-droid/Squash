# Imagen base de ASP.NET Core para Linux
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Squash.WebAPI.csproj /src/
RUN dotnet restore "./Squash.WebAPI.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "./Squash.WebAPI.csproj" -c Release -o /app/build

# Fase de publicación
FROM build AS publish
RUN dotnet publish "./Squash.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Fase final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Squash.WebAPI.dll"]
