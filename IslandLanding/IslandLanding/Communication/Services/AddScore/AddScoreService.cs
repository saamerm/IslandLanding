using IslandLanding.Communication.RequestModel;
using IslandLanding.Communication.ResponseModel;
using IslandLanding.Helper;
using IslandLanding.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IslandLanding.Communication.Services.AddScore
{
  public class AddScoreService : IAddScoreService
  {
    public AddScoreService()
    {

    }
    public  async Task<AddScoreResponseModel> AddScore(AddScoreRequestModel requestModel)
    {//TODO see why refit not work for post 
      // var httpClient = new HttpClient(Helper.Constants.Api_Key);
      //var api = RestService.For<IIslandingApi>("https://script.google.com/macros/s");
      //var model = JsonConvert.SerializeObject(requestModel);
      //var result = await api.PostScore(requestModel);
      //var data = JsonConvert.DeserializeObject<AddScoreResponseModel>(result);
      var client = new HttpClient();
      var data = new AddScoreRequestModel { Name = requestModel.Name, Score = requestModel.Score ,Difficuilty=requestModel.Difficuilty};
      var jsonString = JsonConvert.SerializeObject(data);
      var requestContent = new StringContent(jsonString);
      var response = client.PostAsync(Helper.Constants.Api_Key, requestContent).Result;
      System.Console.WriteLine(response.StatusCode);
     
        var result = response.Content.ReadAsStringAsync().Result;
        System.Console.WriteLine(result);
        var scoreItems = JsonConvert.DeserializeObject<AddScoreResponseModel>(result);
        System.Console.WriteLine(scoreItems.Message + scoreItems.Status);
      //}
      return scoreItems;
    }
  }
}
