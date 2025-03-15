using System;
using System.Collections.Generic;

namespace BackEnd.Model;

public partial class Personrole
{
    public int Userroleid { get; set; }

    public int? Userid { get; set; }

    public int? Roleid { get; set; }

    public virtual Role? Role { get; set; }

    public virtual Person? User { get; set; }
}
