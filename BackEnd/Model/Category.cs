﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackEnd.Model;

public partial class Category
{
    [JsonPropertyName("categoryid")]
    public int Categoryid { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("createdat")]
    public DateTime? Createdat { get; set; }
    [JsonIgnore]
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
