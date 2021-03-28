using IslandLanding.Communication.RequestModel;
using IslandLanding.Communication.ResponseModel;
using IslandLanding.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IslandLanding.Communication
{
  public interface IIslandingApi
  {
    [Get("")]
    Task<List<LeaderBoardModel>> GetBoard(string difficulity);
    [Post("")]
    Task<string> PostScore([Body] AddScoreRequestModel requestModel);
  }
}
