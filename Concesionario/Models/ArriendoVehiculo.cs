using System;
using System.Collections.Generic;

namespace Concesionario.Models;

public partial class ArriendoVehiculo
{
    public int IdArriendo { get; set; }

    public int IdVehiculo { get; set; }

    public int IdCliente { get; set; }

    public int IdSucursal { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public decimal MontoTotal { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Usuario IdClienteNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
