# Pruebas Web Api

## Descripción
Este proyecto es un API REST desarrollado en .NET Core que permite gestionar un maestro de productos. Incluye operaciones para insertar, actualizar y consultar productos, además de características como:
- Persistencia de datos en un archivo JSON.
- Documentación automática con Swagger.
- Middleware para registrar tiempos de respuesta.
- Uso de patrones como Repository Pattern y principios SOLID.

## Funcionalidades
1. **CRUD de Productos**:
   - Crear, actualizar y consultar productos por ID.
2. **Cache**:
   - Diccionario en memoria para los estados del producto (`Active` e `Inactive`).
3. **Log de tiempos de respuesta**:
   - Middleware que registra cada solicitud en `request_logs.txt`.

## Requisitos
- **.NET Core 8.0** o superior
- **Visual Studio 2022** o CLI de .NET

## Estructura del Proyecto
El proyecto está dividido en los siguientes módulos y carpetas:

1. **BusinessLogic**
	- Contiene la lógica de negocio y las interfaces necesarias para implementar los servicios de la API.

2. **DataClasses**
	- Contiene clases y estructuras base utilizadas por el proyecto.

3. **PruebaWebApi**
	- Proyecto principal que contiene la configuración y los controladores para la API.

4. **UnitTest**
	- Contiene pruebas unitarias para validar el comportamiento de las funciones implementadas.

## Endpoints
- **GET /api/products/{id}:** Obtiene un producto por ID.
- **POST /api/products:** Crea un nuevo producto.
- **PUT /api/products/{id}:** Actualiza un producto existente.

## Configuración Local
1. Clonar el repositorio:

git clone https://github.com/wilmarbg/prueba-api.git

2. Navegar hasta el directorio del proyecto:

cd prueba-api

3. Restaurar dependencias:

dotnet restore

4. Configuración del almacenamiento JSON:
El archivo de almacenamiento JSON se creará automáticamente al momento de enviar a crear un producto