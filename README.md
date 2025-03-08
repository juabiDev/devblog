# README - DevBlog

## 📌 Requisitos previos / Prerequisites

Antes de comenzar, asegúrate de tener instaladas las siguientes herramientas:  
Before starting, make sure you have the following tools installed:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) o [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)  
  or [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [DBeaver](https://dbeaver.io/) (opcional, si prefieres otro manejador de base de datos)  
  (optional, if you prefer another database manager)
- [Git](https://git-scm.com/downloads)

## 🚀 Clonar el repositorio / Clone the repository

```sh
git clone https://github.com/juabiDev/devblog.git
cd DevBlog
```

## 🔧 Configurar las variables de entorno / Set environment variables

Crea un archivo `.env` en la raíz del proyecto y añade las siguientes variables:  
Create a `.env` file at the root of the project and add the following variables:

```env
DB_HOST=localhost
DB_PORT=1433
DB_NAME=TuBaseDeDatos / YourDatabaseName
DB_USER=sa
DB_PASSWORD=TuContraseñaSegura / YourSecurePassword
JWT_SECRET=clave-super-secreta / super-secret-key
```

Asegúrate de modificar los valores según tu configuración.  
Make sure to modify the values according to your setup.

## 📦 Instalar dependencias / Install dependencies

```sh
dotnet restore
```

## 🛠️ Generar y Aplicar Migraciones a la Base de Datos / Generate and Apply Database Migrations

Las migraciones no están incluidas en el repositorio, por lo que debes generarlas manualmente.  
Migrations are not included in the repository, so you must generate them manually.

Ejecuta los siguientes comandos:  
Run the following commands:

```sh
dotnet ef migrations add InitialMigration
```

Luego, aplica las migraciones:  
Then, apply the migrations:

```sh
dotnet ef database update
```

Si en el futuro necesitas agregar más cambios en la base de datos, recuerda crear nuevas migraciones con:  
If you need to add more changes to the database in the future, remember to create new migrations with:

```sh
dotnet ef migrations add NombreDeLaMigracion / MigrationName
```

## 🐳 Levantar los servicios con Docker / Start services with Docker

Si la aplicación usa Docker, ejecuta:  
If the application uses Docker, run:

```sh
docker-compose up -d
```

Esto iniciará los contenedores necesarios, como la base de datos SQL Server.  
This will start the necessary containers, such as the SQL Server database.

Para verificar que los contenedores están corriendo:  
To check that the containers are running:

```sh
docker ps
```

Si necesitas detener los contenedores:  
If you need to stop the containers:

```sh
docker-compose down
```

## ▶️ Ejecutar la API / Run the API

```sh
dotnet run
```

Esto iniciará el servidor en `http://localhost:5000` (o el puerto que hayas configurado).  
This will start the server at `http://localhost:5000` (or the port you have configured).

## 📡 Probar la API / Test the API

Puedes probar los endpoints con herramientas como [Postman](https://www.postman.com/) o [Swagger](https://swagger.io/).  
You can test the endpoints with tools like [Postman](https://www.postman.com/) or [Swagger](https://swagger.io/).

Si Swagger está habilitado, puedes acceder en:  
If Swagger is enabled, you can access it at:

```
http://localhost:5000/swagger
```

## 🔄 Actualizar el Proyecto / Update the Project

Para actualizar a la última versión del código, ejecuta:  
To update to the latest version of the code, run:

```sh
git pull origin main
```

Luego, si hay cambios en las migraciones:  
Then, if there are changes in the migrations:

```sh
dotnet ef migrations add NombreDeLaNuevaMigracion / NewMigrationName
```
```sh
dotnet ef database update
```

Si hay cambios en Docker:  
If there are changes in Docker:

```sh
docker-compose up --build -d
```

## 📜 Notas Adicionales / Additional Notes

- Asegúrate de que Docker Desktop esté ejecutándose antes de levantar los servicios.  
  Make sure Docker Desktop is running before starting the services.
- Revisa las configuraciones en `appsettings.json` si necesitas cambiar algún parámetro.  
  Check the settings in `appsettings.json` if you need to change any parameters.
- En caso de errores con la base de datos, revisa la conexión en SQL Server Management Studio o DBeaver.  
  In case of database errors, check the connection in SQL Server Management Studio or DBeaver.



