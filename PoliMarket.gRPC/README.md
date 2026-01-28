# PoliMarket gRPC API

API gRPC para gestión de productos usando .NET 8 y Protocol Buffers.

## Requisitos

- .NET 8.0 SDK
- Postman (para testing gRPC)

## Instalación

```bash
cd unidad-04/gRPC/PoliMarket.gRPC
dotnet restore
dotnet build
```

## Ejecución

```bash
dotnet run
```

Servidor disponible en: `http://localhost:5187`

## Uso de la API

### Configuración en Postman

1. Crear nueva solicitud gRPC
2. Server URL: `localhost:5187`
3. Importar proto: `Protos/producto.proto`
4. Seleccionar servicio: `producto.ProductoService`

### Operaciones Disponibles

#### GetProductos (Listar todos)

Método: `GetProductos`

Mensaje:
```json
{}
```

#### GetProductoById (Obtener por ID)

Método: `GetProductoById`

Mensaje:
```json
{
  "id": "PROD001"
}
```

#### CreateProducto (Crear)

Método: `CreateProducto`

Mensaje:
```json
{
  "nombre": "Auriculares Bluetooth",
  "descripcion": "Auriculares inalámbricos con cancelación de ruido",
  "precio": 250000
}
```

#### UpdateProducto (Actualizar)

Método: `UpdateProducto`

Mensaje:
```json
{
  "id": "PROD001",
  "nombre": "Laptop Dell XPS",
  "precio": 3000000
}
```

#### DeleteProducto (Eliminar)

Método: `DeleteProducto`

Mensaje:
```json
{
  "id": "PROD002"
}
```

## Estructura del Proyecto

```
PoliMarket.gRPC/
├── Data/
│   └── AppDbContext.cs      # Contexto EF Core + SQLite
├── Models/
│   └── Producto.cs          # Entidad Producto
├── Protos/
│   └── producto.proto       # Definición del servicio gRPC
├── Services/
│   └── ProductoService.cs   # Implementación del servicio
├── Program.cs               # Configuración del servidor
└── polimarket_grpc.db       # Base de datos SQLite
```

## Definición del Servicio (Proto)

```protobuf
service ProductoService {
  rpc GetProductos (Empty) returns (ProductoListResponse);
  rpc GetProductoById (GetProductoByIdRequest) returns (ProductoResponse);
  rpc CreateProducto (CreateProductoRequest) returns (ProductoResponse);
  rpc UpdateProducto (UpdateProductoRequest) returns (ProductoResponse);
  rpc DeleteProducto (DeleteProductoRequest) returns (DeleteProductoResponse);
}
```

## Tecnologías

- .NET 8.0
- Grpc.AspNetCore 2.57.0
- Entity Framework Core 8.0.0
- SQLite
- Protocol Buffers (proto3)

