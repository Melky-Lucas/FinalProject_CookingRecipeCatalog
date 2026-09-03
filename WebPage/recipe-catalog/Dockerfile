# Etapa 1: Compilar la aplicación Angular
FROM node:lts AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build -- --configuration=production

# Etapa 2: Servir la aplicación con Nginx
FROM nginx:alpine
# Copiar los archivos compilados de Angular al directorio por defecto de Nginx
COPY --from=build /app/dist/recipe-catalog/browser /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
