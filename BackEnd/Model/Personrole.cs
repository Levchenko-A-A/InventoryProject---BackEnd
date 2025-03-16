using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackEnd.Model;

public partial class Personrole
{
    [JsonPropertyName("userroleid")]
    public int Userroleid { get; set; }
    [JsonPropertyName("userid")]
    public int? Userid { get; set; }
    [JsonPropertyName("roleid")]
    public int? Roleid { get; set; }
    [JsonIgnore]
    public virtual Role? Role { get; set; }
    [JsonIgnore]
    public virtual Person? User { get; set; }
}
