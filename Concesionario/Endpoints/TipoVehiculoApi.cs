using Concesionario.Models;
using Microsoft.EntityFrameworkCore;

namespace Concesionario.Endpoints;

public static class TipoVehiculoApi
{
    public static RouteGroupBuilder MapTipoVehiculoApi(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tipo-vehiculo").WithTags("TipoVehiculo");

        group.MapGet("/", async (ConcesionariodbContext db) =>
        {
            var tipos = await db.TipoVehiculos
                .AsNoTracking()
                .OrderBy(t => t.IdTipoVehiculo)
                .Select(t => new TipoVehiculoResponse(t.IdTipoVehiculo, t.Nombre))
                .ToListAsync();

            return Results.Ok(tipos);
        });

        group.MapPost("/", async (CreateTipoVehiculoRequest request, ConcesionariodbContext db) =>
        {
            var nombre = request.Nombre?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                return Results.BadRequest(new { mensaje = "El nombre es obligatorio." });
            }

            var existe = await db.TipoVehiculos.AnyAsync(t => t.Nombre == nombre);
            if (existe)
            {
                return Results.Conflict(new { mensaje = "Ya existe un tipo de vehículo con ese nombre." });
            }

            var tipoVehiculo = new TipoVehiculo
            {
                Nombre = nombre
            };

            db.TipoVehiculos.Add(tipoVehiculo);
            await db.SaveChangesAsync();

            return Results.Created(
                $"/api/tipo-vehiculo/{tipoVehiculo.IdTipoVehiculo}",
                new TipoVehiculoResponse(tipoVehiculo.IdTipoVehiculo, tipoVehiculo.Nombre));
        });

        return group;
    }

    private sealed record CreateTipoVehiculoRequest(string Nombre);
    private sealed record TipoVehiculoResponse(int IdTipoVehiculo, string Nombre);
}
