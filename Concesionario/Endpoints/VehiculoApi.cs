using Concesionario.Models;
using Microsoft.EntityFrameworkCore;


namespace concesionario.Endpoints
{
    public static class VehiculoApi
    {
        public static void MapVehiculoApi(this WebApplication app)
        {
            var vehiculos = app.MapGroup("/api/vehiculos")
                .WithTags("Vehículos")
                .RequireAuthorization();

            // GET: Obtener todos los vehículos
            vehiculos.MapGet("/", async (ConcesionariodbContext db) =>
            {
                var listaVehiculos = await db.Vehiculos.ToListAsync();

                return Results.Ok(listaVehiculos);
            })
            .WithName("GetVehiculos");

            // GET: Obtener vehículo por ID
            vehiculos.MapGet("/{id:int}", async (int id, ConcesionariodbContext db) =>
            {
                var vehiculo = await db.Vehiculos.FindAsync(id);

                if (vehiculo is null)
                    return Results.NotFound("Vehículo no encontrado");

                return Results.Ok(vehiculo);
            })
            .WithName("GetVehiculoById");

            // GET: Buscar vehículos por marca
            vehiculos.MapGet("/marca/{idMarca:int}", async (
                int idMarca,
                ConcesionariodbContext db) =>
            {
                var vehiculosPorMarca = await db.Vehiculos
                    .Where(v => v.IdMarca == idMarca)
                    .ToListAsync();

                if (!vehiculosPorMarca.Any())
                    return Results.NotFound("No hay vehículos de esa marca");

                return Results.Ok(vehiculosPorMarca);
            })
            .WithName("GetVehiculosPorMarca");

            // POST: Crear nuevo vehículo
            vehiculos.MapPost("/", async (
                Vehiculo vehiculo,
                ConcesionariodbContext db) =>
            {
                db.Vehiculos.Add(vehiculo);

                await db.SaveChangesAsync();

                return Results.Created(
                    $"/api/vehiculos/{vehiculo.IdVehiculo}",
                    vehiculo);
            })
            .WithName("CreateVehiculo")
            .RequireAuthorization(policy =>
                policy.RequireRole("admin"));

            // PUT: Actualizar vehículo
            vehiculos.MapPut("/{id:int}", async (
                int id,
                Vehiculo vehiculoActualizado,
                ConcesionariodbContext db) =>
            {
                var vehiculo = await db.Vehiculos.FindAsync(id);

                if (vehiculo is null)
                    return Results.NotFound("Vehículo no encontrado");

                vehiculo.Patente = vehiculoActualizado.Patente;
                vehiculo.Modelo = vehiculoActualizado.Modelo;
                vehiculo.Anio = vehiculoActualizado.Anio;
                vehiculo.Precio = vehiculoActualizado.Precio;
                vehiculo.Estado = vehiculoActualizado.Estado;
                vehiculo.IdMarca = vehiculoActualizado.IdMarca;
                vehiculo.IdTipoVehiculo = vehiculoActualizado.IdTipoVehiculo;
                vehiculo.IdTipoCombustible = vehiculoActualizado.IdTipoCombustible;
                vehiculo.IdSucursal = vehiculoActualizado.IdSucursal;

                await db.SaveChangesAsync();

                return Results.Ok(vehiculo);
            })
            .WithName("UpdateVehiculo")
            .RequireAuthorization(policy =>
                policy.RequireRole("admin"));

            // DELETE: Eliminar vehículo
            vehiculos.MapDelete("/{id:int}", async (
                int id,
                ConcesionariodbContext db) =>
            {
                var vehiculo = await db.Vehiculos.FindAsync(id);

                if (vehiculo is null)
                    return Results.NotFound("Vehículo no encontrado");

                db.Vehiculos.Remove(vehiculo);

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("DeleteVehiculo")
            .RequireAuthorization(policy =>
                policy.RequireRole("admin"));
        }
    }
}
