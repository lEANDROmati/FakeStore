							Proyecto FakeStore

# Descripción General :

API desarrollada en .NET para gestionar una tienda ficticia. Este proyecto te permitirá manejar productos, categorías, usuarios y roles de una forma fácil y eficiente. Todo está diseñado siguiendo principios de **arquitectura limpia**, con una separación clara de las capas de **aplicación**, **dominio** e **infraestructura**.

### __Herramientas y Tecnologías:__

- Framework: .NET 8.

- ORM: Entity Framework Core (con enfoque en Code First).

- Autenticación: JSON Web Tokens (JWT).

- Documentación: Swagger .

- Base de Datos: SQL Server .
  
- Validación mediante Data Annotations.
  
	**Principios de Diseño**

	- Separación de responsabilidades.

	- Inyección de dependencias.

	- Arquitectura limpia.

	- DTO (Data Transfer Objects).

## Estructura del Proyecto ##
El proyecto está organizado en cuatro capas principales:


 ### Application ###
 Esta capa contiene la lógica de la aplicación, incluyendo servicios, modelos y interfaces necesarias para interactuar con las diferentes funcionalidades del dominio.
 
**Carpetas principales:**
- **Models:** Modelos que definen las estructuras de datos utilizadas en la comunicación entre la API y el cliente. Está subdividida por categorías lógicas:
	- Categories: Modelos para solicitudes y respuestas relacionadas con las categorías.
	- Login: Modelos relacionados con la autenticación y gestión de sesiones.
	- Products y Users: Estructuras similares para sus respectivas entidades.
- **Services:** Contiene los servicios que implementan la lógica de aplicación, divididos en:
	- Interfaces (IService): Abstracciones para los servicios.
	- Implementaciones (Service): Lógica concreta para manejar productos, categorías, usuarios, autenticación (JWT), etc.

### Domain ###
Define las entidades centrales del proyecto y las interfaces para los repositorios. Es independiente de cualquier tecnología de acceso a datos.
**Carpetas principales:**
- **Entities:** Representan las tablas o modelos principales de la base de datos:
	- CategoryEntity: Define las categorías de productos.
	- ProductsEntity: Modela los productos.
	- RoleEntity: Representa los roles de usuario.
	- UserEntity: Describe a los usuarios.
- **Repositories:** Contiene las interfaces que definen las operaciones CRUD y otros métodos específicos.

### Domain.Infrastructure ###
Implementa los detalles específicos de la persistencia de datos y la configuración de la base de datos.

**Carpetas principales:**
- **Data:** Contiene la configuración del contexto de datos (DataContext), utilizado por Entity Framework para gestionar la base de datos.
- **Migrations**: Archivos generados automáticamente por Entity Framework para mantener el esquema de la base de datos actualizado.
- **Repositories**: Implementaciones concretas de las interfaces de repositorios definidas en Domain.

### FakeStoreApi ###
La capa principal que expone las funcionalidades del sistema a través de controladores y endpoints.

**Carpetas principales:**
- Controllers: Contiene los controladores RESTful, cada uno responsable de manejar una entidad del sistema:
- Archivos clave:
 -Program.cs y appsettings.json



## swagger ui ##

[visita swagger](https://fakestoreapi-f2eagtbeh3euasc6.canadacentral-01.azurewebsites.net)
 
![image](https://github.com/user-attachments/assets/5689aa79-c5b7-4a7b-82fa-95ee644065d8)
