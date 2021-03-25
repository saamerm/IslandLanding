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
      var httpClient = new HttpClient(new HttpLoggingHandler()) { BaseAddress = new System.Uri(Helper.Constants.Api_Key) };
      var api = RestService.For<IIslandingApi>(httpClient);
      var result = await api.GetBoard();
      return result;
    }
  }
}
