using Concesionario.Endpoints;
using Concesionario.Models;

using Scalar.AspNetCore;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
<<<<<<< HEAD
using concesionario.Endpoints;
=======
using Concesionario.Endpoints;
>>>>>>> 1b2b4a605c76548b61ce91e353b7a5350d6be88b

var builder = WebApplication.CreateBuilder(args);

//var jwtKey = builder.Configuration["Jwt:Key"];
//var jwtIssuer = builder.Configuration["Jwt:Issuer"];
//var jwtAudince = builder.Configuration["Jwt:Audience"];


/*builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudince,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtKey))

            };
        });
builder.Services.AddAuthorization();*/

builder.Services.AddDbContext<ConcesionariodbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("miconexion")));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//app.UseAuthentication(); // ¿quién es?
//app.UseAuthorization(); // ¿qué puede hacer?

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapRolApi();
app.MapMarcaApi();
app.MapCombustibleApi();
app.MapTipoVehiculoApi();
app.MapSucursalApi();
app.MapUsuarioApi();
<<<<<<< HEAD
app.MapVentaApi();
app.MapVehiculoApi();
=======
app.MapMantencionApi();
app.MapVehiculoApi();

>>>>>>> 1b2b4a605c76548b61ce91e353b7a5350d6be88b
app.Run();