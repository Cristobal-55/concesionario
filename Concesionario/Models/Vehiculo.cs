using System;
using System.Collections.Generic;

namespace Concesionario.Models;

public partial class Vehiculo
{
    public int IdVehiculo { get; set; }

    public string? Patente { get; set; }

    public string Modelo { get; set; } = null!;

    public int Anio { get; set; }

    public decimal Precio { get; set; }

    public string? Estado { get; set; }

    public int IdMarca { get; set; }

    public int IdTipoVehiculo { get; set; }

    public int IdTipoCombustible { get; set; }

    public int IdSucursal { get; set; }

    public virtual ICollection<ArriendoVehiculo> ArriendoVehiculos { get; set; } = new List<ArriendoVehiculo>();

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    public virtual TipoCombustible IdTipoCombustibleNavigation { get; set; } = null!;

    public virtual TipoVehiculo IdTipoVehiculoNavigation { get; set; } = null!;

    public virtual ICollection<Mantencion> Mantencions { get; set; } = new List<Mantencion>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
