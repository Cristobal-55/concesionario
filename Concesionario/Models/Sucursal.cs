using System;
using System.Collections.Generic;

namespace Concesionario.Models;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Ciudad { get; set; } = null!;

    public virtual ICollection<ArriendoVehiculo> ArriendoVehiculos { get; set; } = new List<ArriendoVehiculo>();

    public virtual ICollection<Mantencion> Mantencions { get; set; } = new List<Mantencion>();

    public virtual ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
