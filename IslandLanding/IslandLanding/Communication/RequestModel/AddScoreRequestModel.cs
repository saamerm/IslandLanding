using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IslandLanding.Communication.RequestModel
{
 public class AddScoreRequestModel
  {
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "Score")]
    public string Score { get; set; }
    [JsonProperty(PropertyName = "Difficuilty")]
    public string Difficuilty { get; set; }
  }
}
