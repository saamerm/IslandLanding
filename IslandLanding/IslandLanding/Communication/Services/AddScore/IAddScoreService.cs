using IslandLanding.Communication.RequestModel;
using IslandLanding.Communication.ResponseModel;
using IslandLanding.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IslandLanding.Communication.Services.AddScore
{
  public interface IAddScoreService
  {
    Task<AddScoreResponseModel> AddScore(AddScoreRequestModel requestModel);
  }
}
