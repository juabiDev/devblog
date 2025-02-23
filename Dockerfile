# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DevBlog/DevBlog.csproj", "DevBlog/"]
RUN dotnet restore "./DevBlog/DevBlog.csproj"
COPY . .
WORKDIR "/src/DevBlog"
RUN dotnet build "./DevBlog.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevBlog.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevBlog.dll"]

ARG DB_HOST
ARG DB_PORT
ARG DB_USER
ARG DB_PASSWORD
ARG DB_NAME

ENV DB_HOST=${DB_HOST}
ENV DB_PORT=${DB_PORT}
ENV DB_USER=${DB_USER}
ENV DB_PASSWORD=${DB_PASSWORD}
ENV DB_NAME=${DB_NAME}

# Esta fase se usa para la base de datos de desarrollo
FROM mcr.microsoft.com/mssql/server:2019-latest AS db
USER $APP_UID
WORKDIR /app
EXPOSE 1433
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=${DB_PASSWORD}
ENV MSSQL_PID=Express
ENV MSSQL_TCP_PORT=1433
ENV MSSQL_DB=${DB_NAME}
ENV MSSQL_USER=${DB_USER}
ENV MSSQL_PASSWORD=${DB_PASSWORD}
ENV MSSQL_DATA=/var/opt/mssql/data
ENV MSSQL_LOG=/var/opt/mssql/log
ENV MSSQL_BACKUP=/var/opt/mssql/backup
ENV MSSQL_SEED=/var/opt/mssql/seed
ENV MSSQL_SCRIPTS=/var/opt/mssql/scripts
ENV MSSQL_CONFIG=/var/opt/mssql/config
ENV MSSQL_TOOLS=/opt/mssql-tools/bin
ENV PATH=$PATH:/opt/mssql-tools/bin
ENV MSSQL_AGENT=/opt/mssql/bin
