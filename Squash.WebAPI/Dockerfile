# Imagen base para ejecutar la API
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagen para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY ./Squash.WebAPI.csproj ./
RUN dotnet restore Squash.WebAPI.csproj

# Copiar el resto de los archivos y construir el proyecto
COPY . .
RUN dotnet build Squash.WebAPI.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publicar el proyecto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish Squash.WebAPI.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen base para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Squash.WebAPI.dll"]
