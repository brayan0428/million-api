# Proyecto de Gestión de Propiedades

Este proyecto es una aplicación web para la gestión de propiedades inmobiliarias. Permite a los usuarios gestionar propietarios, propiedades, imágenes de propiedades y trazas de propiedades.

## Tecnologías Utilizadas

- **ASP.NET Core**: Framework principal para la construcción de la aplicación web.
- **Entity Framework Core**: ORM para el acceso a la base de datos.
- **AutoMapper**: Biblioteca para el mapeo de objetos.
- **Swagger**: Herramienta para la documentación de la API.
- **SQL Server**: Base de datos relacional.

## Estructura del Proyecto

El proyecto está dividido en varias capas para una mejor organización y separación de responsabilidades:

- **Core**: Contiene las interfaces y entidades del dominio.
- **Infrastructure**: Implementaciones de repositorios y contexto de base de datos.
- **Services**: Implementaciones de servicios de negocio.
- **Web**: Proyecto principal de la aplicación web que contiene controladores y configuraciones.

## Configuración del Proyecto

### Requisitos Previos

- .NET 8 SDK
- SQL Server

### Configuración de la Base de Datos

Asegúrate de tener una instancia de SQL Server en ejecución , una base de datos llamada MILLION y actualiza la cadena de conexión en `appsettings.json`:
~~~
server=your_server; database=MILLION; user id=your_user;password=your_password;TrustServerCertificate=True
~~~

### Migraciones de Base de Datos

Para aplicar las migraciones de la base de datos, ejecuta los siguientes comandos en la consola del administrador de paquetes o en la terminal:
~~~
dotnet ef database update -p Infrastructure -s Web
~~~

### Ejecución del Proyecto

Para ejecutar el proyecto, utiliza el siguiente comando:
~~~
dotnet run --project Web
~~~


La aplicación estará disponible en `https://localhost:5001` y la documentación de Swagger en `https://localhost:5001/swagger`.






