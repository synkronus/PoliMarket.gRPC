using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PoliMarket.gRPC.Data;

namespace PoliMarket.gRPC.Services;

public class ProductoGrpcService : ProductoService.ProductoServiceBase
{
    private readonly AppDbContext _context;

    public ProductoGrpcService(AppDbContext context)
    {
        _context = context;
    }

    public override async Task<ProductoListResponse> GetProductos(Empty request, ServerCallContext context)
    {
        var productos = await _context.Productos.ToListAsync();
        var response = new ProductoListResponse();
        
        foreach (var p in productos)
        {
            response.Productos.Add(new ProductoMessage
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio
            });
        }

        return response;
    }

    public override async Task<ProductoResponse> GetProductoById(GetProductoByIdRequest request, ServerCallContext context)
    {
        var producto = await _context.Productos.FindAsync(request.Id);
        
        if (producto == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Producto no encontrado"));
        }

        return new ProductoResponse
        {
            Producto = new ProductoMessage
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio
            }
        };
    }

    public override async Task<ProductoResponse> CreateProducto(CreateProductoRequest request, ServerCallContext context)
    {
        var producto = new Models.Producto
        {
            Id = Guid.NewGuid().ToString(),
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Precio = request.Precio
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return new ProductoResponse
        {
            Producto = new ProductoMessage
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio
            }
        };
    }

    public override async Task<ProductoResponse> UpdateProducto(UpdateProductoRequest request, ServerCallContext context)
    {
        var producto = await _context.Productos.FindAsync(request.Id);
        
        if (producto == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Producto no encontrado"));
        }

        if (request.HasNombre) producto.Nombre = request.Nombre;
        if (request.HasDescripcion) producto.Descripcion = request.Descripcion;
        if (request.HasPrecio) producto.Precio = request.Precio;

        await _context.SaveChangesAsync();

        return new ProductoResponse
        {
            Producto = new ProductoMessage
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio
            }
        };
    }

    public override async Task<DeleteProductoResponse> DeleteProducto(DeleteProductoRequest request, ServerCallContext context)
    {
        var producto = await _context.Productos.FindAsync(request.Id);
        
        if (producto == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Producto no encontrado"));
        }

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();

        return new DeleteProductoResponse { Success = true };
    }
}

