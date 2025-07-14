# Imagen base para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagen para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar todo el proyecto (incluido DevBlog.sln y todos los proyectos referenciados)
COPY . .

# Restaurar y compilar usando el archivo de solución
RUN dotnet restore "DevBlog.sln"
RUN dotnet build "DevBlog.sln" -c $BUILD_CONFIGURATION -o /app/build

# Publicar solo DevBlog.csproj, pero con todas las dependencias
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DevBlog/DevBlog.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevBlog.dll"]
