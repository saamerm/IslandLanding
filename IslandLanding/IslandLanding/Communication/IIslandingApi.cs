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
    Task<List<LeaderBoardModel>> GetBoard();
  }
}
