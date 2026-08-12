# .NET 10 Web API

API REST desarrollada con **.NET 10 (LTS)** y ASP.NET Core, utilizando OpenAPI para la documentación interactiva.

## 🚀 Prerrequisitos
* [.NET 10 SDK](https://dotnet.microsoft.com/) instalado en tu equipo.
* Una herramienta para probar peticiones HTTP como [Postman](https://postman.com) o usar swagger en development.

## 📦 Instalación y Ejecución
1. Clona el repositorio:
   ```bash
   git clone (https://github.com/Melky-Lucas/FinalProject_CookingRecipeCatalog)
   cd FinalProject_CookingRecipeCatalog
   ```
2. Restaura los paquetes NuGet:
   ```bash
   dotnet restore
   ```
3. Ejecuta la aplicación:
   ```bash
   dotnet run
   ```

La API estará disponible por defecto en `https://localhost:7274` o `http://localhost:5123`.

## 📖 Documentación de la API (OpenAPI / Swagger)
Con .NET 10, la especificación OpenAPI se genera de forma nativa. Si está habilitado en entorno de desarrollo, puedes acceder a la interfaz visual o al archivo JSON en:
* `(https://localhost:7274/swagger/index.html)` (o el puerto asignado por tu perfil de ejecución).

## 🛠️ Estructura del Proyecto
* **WebAPIProgram.cs**: Configuración del pipeline HTTP y servicios (Inyección de Dependencias).
* **WebAPI/Controllers/** o **Endpoints/**: Definición de los recursos y rutas de la API.
* **Core/Models/**: Entidades de datos de la app.
* **WebAPI/appsettings.json**: Configuración de la aplicación y cadenas de conexión.

## 🧪 Pruebas
Ejecuta la suite de pruebas unitarias o de integración con:
```bash
dotnet test
```
