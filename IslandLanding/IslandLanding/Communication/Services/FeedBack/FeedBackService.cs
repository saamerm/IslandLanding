using IslandLanding.Communication.RequestModel;
using IslandLanding.Communication.ResponseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IslandLanding.Communication.Services.FeedBack
{
 public class FeedBackService
  {
    public FeedBackService()
    {

    }
    public async Task<AddFeedBcakResponseModel> AddFeedback(AddFeedbackRequestModel requestModel)
    {
      var client = new HttpClient();
      var data = new AddFeedbackRequestModel { Name = requestModel.Name, Email = requestModel.Email, Feedback = requestModel.Feedback };
      var jsonString = JsonConvert.SerializeObject(data);
      var requestContent = new StringContent(jsonString);
      var response = client.PostAsync(Helper.Constants.Feedback_Api_Key, requestContent).Result;
      System.Console.WriteLine(response.StatusCode);

      var result = response.Content.ReadAsStringAsync().Result;
      System.Console.WriteLine(result);
      var feedbackAdded = JsonConvert.DeserializeObject<AddFeedBcakResponseModel>(result);
      return feedbackAdded;
    }
  }
}

