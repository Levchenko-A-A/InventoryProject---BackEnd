using System;
using System.Collections.Generic;

namespace BackEnd.Model;

public partial class Device
{
    public int Deviceid { get; set; }

    public string Name { get; set; } = null!;

    public int? Categoryid { get; set; }

    public int? Manufacturerid { get; set; }

    public int? Locationid { get; set; }

    public string? Description { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Barcode> Barcodes { get; set; } = new List<Barcode>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Inventorynumber> Inventorynumbers { get; set; } = new List<Inventorynumber>();

    public virtual Location? Location { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }
}
