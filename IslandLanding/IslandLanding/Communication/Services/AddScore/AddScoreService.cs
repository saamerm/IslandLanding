using IslandLanding.Communication.RequestModel;
using IslandLanding.Communication.ResponseModel;
using IslandLanding.Helper;
using IslandLanding.Models;
using Newtonsoft.Json;
using Refit;
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
    {
      var httpClient = new HttpClient(new HttpLoggingHandler()) { BaseAddress = new System.Uri(Helper.Constants.Api_Key) };
      var api = RestService.For<IIslandingApi>(httpClient);
      var result = await api.PostScore(requestModel);
      var data = JsonConvert.DeserializeObject<AddScoreResponseModel>(result);
      return data;
    }
  }
}
