using IslandLanding.Helper;
using IslandLanding.Models;
using Newtonsoft.Json;
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
    public async Task<List<LeaderBoardModel>> GetBoard(string difficulity)
    {
      //ToDo see why refit get wrong result
      //var httpClient = new HttpClient(new HttpLoggingHandler()) { BaseAddress = new System.Uri("https://script.google.com/macros/s/AKfycbz4drxjjFR5dB8-WT1KZ0RTT4cgDB1WPt5MTswsFAnFO7N2FdFOh1G6ZrnSf7hv3Uul/exec?DifficuiltyLevel=Hard") };
      //var api = RestService.For<IIslandingApi>("https://script.google.com/macros/s/AKfycbwI5RbJRhki8-TcdwSWoAqInEGOEWDs7KhfKkF7rvwV_l_ilRmV7G5ySNhESnPVEHk/exec?difficuiltyLevel=Hard");
      //var result = await api.GetBoard();
      var url =Constants.Api_Key+ "?difficuiltyLevel="+difficulity;
      var client = new HttpClient();
      var response = await client.GetAsync(url);
      var result = await response.Content.ReadAsStringAsync();
      var leaderBoardList = JsonConvert.DeserializeObject<List<LeaderBoardModel>>(result);
      return leaderBoardList;
    }
  }
}
