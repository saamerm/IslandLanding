using IslandLanding.Helper;
using IslandLanding.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IslandLanding.Communication.Services
{
  public class GetLeaderBoardService : IGetLeaderBoardService
  {
    public GetLeaderBoardService()
    {

    }
    public async Task<List<LeaderBoardModel>> GetBoard()
    {
      var httpClient = new HttpClient(new HttpLoggingHandler()) { BaseAddress = new System.Uri("https://script.google.com/macros/s/AKfycbzZRx3xB7mUxrCQyP6ZEtWBtzlXv97q1vHOxSJ27pQ8TgL2Jm68etoNnADcBYzAMkdq/exec") };
      var api = RestService.For<IIslandingApi>(httpClient);
      
      var result = await api.GetBoard();
      return result;
    }
  }
}
