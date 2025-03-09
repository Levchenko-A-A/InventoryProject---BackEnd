using System;
using System.Collections.Generic;

namespace BackEnd.Model;

public partial class Location
{
    public int Locationid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
