using System;
using System.Collections.Generic;

namespace Concesionario.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int IdVehiculo { get; set; }

    public int IdCliente { get; set; }

    public int IdVendedor { get; set; }

    public int IdSucursal { get; set; }

    public DateTime? FechaVenta { get; set; }

    public decimal MontoTotal { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Usuario IdClienteNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;

    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;

    public virtual Usuario IdVendedorNavigation { get; set; } = null!;
}
