using IslandLanding.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IslandLanding.Communication.Services
{
 public class GetGoogleSheetUrlService
  {
    public GetGoogleSheetUrlService()
    {

    }
    public async Task<string> GetUrl()
    {
      var url = Constants.Feedback_Api_Key;
      var client = new HttpClient();
      var response = await client.GetAsync(url);
      var result = await response.Content.ReadAsStringAsync();
      var apiUrl = JsonConvert.DeserializeObject(result).ToString();
      return apiUrl;
    }
  }
}
