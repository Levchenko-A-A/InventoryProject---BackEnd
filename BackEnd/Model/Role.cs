using System;
using System.Collections.Generic;

namespace BackEnd.Model;

public partial class Role
{
    public int Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Personrole> Personroles { get; set; } = new List<Personrole>();
}
