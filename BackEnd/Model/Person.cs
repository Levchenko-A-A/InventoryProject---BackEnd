using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackEnd.Model;

public partial class Person
{
    [JsonPropertyName("personid")]
    public int Personid { get; set; }
    [JsonPropertyName("personname")]
    public string Personname { get; set; } = null!;
    [JsonPropertyName("passwordhash")]
    public string Passwordhash { get; set; } = null!;
    [JsonPropertyName("salt")]
    public string Salt { get; set; } = null!;
    [JsonPropertyName("createdat")]
    public DateTime? Createdat { get; set; }
}
