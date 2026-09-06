using System;
using System.Collections.Generic;

namespace Concesionario.Models;

public partial class TipoCombustible
{
    public int IdTipoCombustible { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
