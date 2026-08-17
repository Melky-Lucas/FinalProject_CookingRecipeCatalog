# Catálogo de Recetas — Frontend Angular

Aplicación web en **Angular 17** (standalone components) que consume la WebAPI REST del proyecto `RecipeCatalog`.

## Requisitos

- Node.js 18+
- npm 9+
- WebAPI en ejecución (`https://localhost:7274`)

## Funcionalidades

| Pantalla | Ruta | Acceso |
|----------|------|--------|
| Catálogo público (10 recetas/página) | `/` | Público |
| Detalle de receta | `/recetas/:id` | Público |
| Inicio de sesión | `/login` | Público |
| Registro | `/registro` | Público |
| Mis recetas (CRUD) | `/mis-recetas` | Autenticado |

## Estructura del proyecto

```
src/app/
├── components/navbar/     # Barra de navegación
├── guards/                # authGuard
├── interceptors/          # JWT Bearer en HttpClient
├── models/                # Interfaces TypeScript
├── pages/                 # Pantallas principales
├── services/              # Auth, Recipe, Catalog
└── utils/                 # Helpers (tiempo, dificultad)
```

## Configuración de la API

La URL base está en `src/environments/environment.ts`:

```typescript
apiUrl: 'https://localhost:7274/api'
```

Si la API usa otro puerto o protocolo, actualice ese archivo.

## Ejecución

1. Inicie la WebAPI (.NET) del repositorio principal.
2. En esta carpeta:

```bash
npm install
npm start
```

3. Abra `http://localhost:4200`.

> **Nota:** Si el navegador bloquea peticiones HTTPS al certificado de desarrollo de la API, acceda primero a `https://localhost:7274` y acepte el certificado.

## Autenticación

- Registro e inicio de sesión contra `/api/Auth/register` y `/api/Auth/login`.
- El token JWT se guarda en `localStorage` y se envía como `Authorization: Bearer {token}`.
- Las rutas bajo `/mis-recetas` están protegidas con `authGuard`.
- No se implementa refresh token (según requisitos).

## Backend adicional

Se añadió el filtro `IsPublic` en `RecipeSearchQuery` para paginar correctamente el catálogo público, y CORS para `http://localhost:4200`.

## Scripts útiles

| Comando | Descripción |
|---------|-------------|
| `npm start` | Servidor de desarrollo |
| `npm run build` | Compilación de producción |
