using IslandLanding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IslandLanding.Communication.Services
{
  public interface IGetLeaderBoardService
  {
    Task <List<LeaderBoardModel>> GetBoard(string difficulity);
  }
}
