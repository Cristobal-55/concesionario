using Concesionario.Models;
using Microsoft.EntityFrameworkCore;
using Concesionario.Dtos; 

namespace Concesionario.Endpoints
{
    public static class UsuarioApi
    {
        public static void MapUsuarioApi(this WebApplication app)
        {
            var usuario = app.MapGroup("/api/Usuario").WithTags("Usuario");

            // 1. Obtener todos los usuarios
            usuario.MapGet("/", async (ConcesionariodbContext db) =>
            {
                var usuarios = await db.Usuarios
                    .Include(u => u.IdRolNavigation)
                    .Select(u => new UsuarioResponseDto(
                        u.IdUsuario,
                        u.Nombre,
                        u.Email,
                        u.Telefono,
                        u.IdRol,
                        u.IdRolNavigation.Nombre // Asumiendo que 'Rol' tiene la propiedad 'Nombre'
                    ))
                    .ToListAsync();

                return Results.Ok(usuarios);
            });

            // 2. Obtener usuario por ID
            usuario.MapGet("/{id:int}", async (int id, ConcesionariodbContext db) =>
            {
                var u = await db.Usuarios
                    .Include(u => u.IdRolNavigation)
                    .FirstOrDefaultAsync(x => x.IdUsuario == id);

                if (u == null) return Results.NotFound();

                var dto = new UsuarioResponseDto(
                    u.IdUsuario,
                    u.Nombre,
                    u.Email,
                    u.Telefono,
                    u.IdRol,
                    u.IdRolNavigation?.Nombre
                );

                return Results.Ok(dto);
            });

            // 3. Crear nuevo usuario
            usuario.MapPost("/", async (CrearUsuarioDto dto, ConcesionariodbContext db) =>
            {
                // Verificar si el correo ya está registrado
                var existeEmail = await db.Usuarios.AnyAsync(u => u.Email == dto.Email);
                if (existeEmail)
                    return Results.BadRequest("El correo electrónico ya se encuentra registrado.");

                var nuevoUsuario = new Usuario
                {
                    Nombre = dto.Nombre,
                    Email = dto.Email,
                    Password = dto.Password, 
                    Telefono = dto.Telefono,
                    IdRol = dto.IdRol
                };

                db.Usuarios.Add(nuevoUsuario);
                await db.SaveChangesAsync();

                var response = new UsuarioResponseDto(
                    nuevoUsuario.IdUsuario,
                    nuevoUsuario.Nombre,
                    nuevoUsuario.Email,
                    nuevoUsuario.Telefono,
                    nuevoUsuario.IdRol,
                    null
                );

                return Results.Created($"/api/Usuario/{nuevoUsuario.IdUsuario}", response);
            });

            // 4. Actualizar usuario existente
            usuario.MapPut("/{id:int}", async (int id, ActualizarUsuarioDto dto, ConcesionariodbContext db) =>
            {
                var exist = await db.Usuarios.FindAsync(id);
                if (exist == null) return Results.NotFound();

                exist.Nombre = dto.Nombre;
                exist.Email = dto.Email;
                exist.Telefono = dto.Telefono;
                exist.IdRol = dto.IdRol;

                await db.SaveChangesAsync();
                return Results.Ok("Usuario actualizado correctamente.");
            });

            // 5. Eliminar usuario
            usuario.MapDelete("/{id:int}", async (int id, ConcesionariodbContext db) =>
            {
                var exist = await db.Usuarios.FindAsync(id);
                if (exist == null) return Results.NotFound();

                db.Usuarios.Remove(exist);
                await db.SaveChangesAsync();

                return Results.NoContent();

            });
        }
    }
}

