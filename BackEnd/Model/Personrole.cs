using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackEnd.Model;

public partial class Personrole
{
    [JsonPropertyName("Personroleid")]
    public int Personroleid { get; set; }
    [JsonPropertyName("Personid")]
    public int? Personid { get; set; }
    [JsonPropertyName("Roleid")]
    public int? Roleid { get; set; }
    [JsonIgnore]
    public virtual Person? Person { get; set; }
    [JsonIgnore]
    public virtual Role? Role { get; set; }
}
