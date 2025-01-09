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
- SQL Server (Con base de datos MILLION creada)
- Tener instalado globalmente dotnet-ef
    - Si no lo tiene instalado, puede hacerlo ejecutando el siguiente comando
        ~~~    
        dotnet tool install --global dotnet-ef
        ~~~


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
# Pruebas Unitarias
Las pruebas unitarias para el servicio de propiedades (`PropertyService`) se encuentran en el archivo `PropertyServiceTests.cs`. 
Las pruebas unitarias para el servicio de propiedades (`PropertyImageService`) se encuentran en el archivo `PropertyImageServiceTests.cs`. 

## Ejecución de las Pruebas

Para ejecutarlas desde el IDE visual studio, sigue estos pasos:

1. Abre el proyecto en Visual Studio.
2. Asegúrate de que el proyecto de pruebas (`Tests`) esté configurado correctamente y que todos los paquetes NuGet necesarios estén instalados.
3. Abre el **Explorador de Pruebas** desde el menú **Pruebas** > **Ventanas** > **Explorador de Pruebas**.
4. En el **Explorador de Pruebas**, haz clic en **Ejecutar Todo** para ejecutar todas las pruebas unitarias.

### Ejecución desde la Línea de Comandos

También puedes ejecutar las pruebas desde la línea de comandos utilizando `dotnet test`. Navega hasta el directorio raíz de la solución y ejecuta el siguiente comando:

~~~
dotnet test
~~~


### Ejecución del Proyecto

Para ejecutar el proyecto, utiliza el siguiente comando:
~~~
dotnet run --project Web
~~~
# Endpoints de la API
### 1. Login
**URL:** `/api/auth/login`  
**Método:** `POST`  
**Descripción:** Permite a un usuario autenticarse para obtener un token y utilizar las demas funcionalidades
**Cuerpo petición**: 
```json
{
  "email": "admin@example.com",
  "password": "Admin123",
}
```

### 2. Obtener todas las propiedades (Requiere JWT Token)
**URL:** `/api/property`  
**Método:** `GET`  
**Descripción:** Obtener listado de todas las propiedades

### 3. Obtener propiedades aplicando filtros (Requiere JWT Token)
**URL:** `/api/property/filter`  
**Query params (Opcionales):** PriceMin | PriceMax | YearMin | YearMax | IdOwner
**Método:** `GET`  
**Descripción:** Obtener listado de todas las propiedades

### 4. Crear propiedad (Requiere JWT Token)
**URL:** `/api/property`  
**Método:** `POST`  
**Descripción:** Crear una propiedad
**Cuerpo petición**: 
```json
{
  "name": "Casa en Miami",
  "address": "Calle 123",
  "price": 95000000,
  "codeInternal": "PROP006",
  "year": 2018,
  "idOwner": 3
}
```

### 5. Actualizar propiedad (Requiere JWT Token)
**URL:** `/api/property/{id}`  
**Método:** `PUT`  
**Descripción:** Actualizar una propiedad
**Cuerpo petición**: 
```json
{
  "name": "Casa en Barranquilla",
  "address": "Calle 123",
  "price": 95000000,
  "codeInternal": "PROP006",
  "year": 2018,
  "idOwner": 3
}
```

### 6. Actualizar precio propiedad (Requiere JWT Token)
**URL:** `/api/property/{id}/price`  
**Método:** `PUT`  
**Descripción:** Actualizar precio de una propiedad
**Cuerpo petición**: 
```json
{
  "price": 80000000
}
```

### 7. Agregar imagen a una propiedad (Requiere JWT Token)
**URL:** `/api/property-image/{idProperty}`  
**Método:** `POST`  
**Descripción:** Agregar imagen a una propiedad
**Cuerpo petición**: Subir una imagen





