# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar el archivo de proyecto y restaurar dependencias
COPY ["Squash.WebAPI.csproj", "Squash.WebAPI/"]
RUN dotnet restore "Squash.WebAPI.csproj"

# Copiar el resto de los archivos y construir el proyecto
COPY Squash.WebAPI/ /src/Squash.WebAPI/
WORKDIR "/src"
RUN dotnet build "Squash.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar el proyecto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Squash.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final: usar la imagen base y copiar los archivos publicados
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Squash.WebAPI.dll"]