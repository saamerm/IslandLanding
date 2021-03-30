using System;
using System.Collections.Generic;
using System.Text;

namespace IslandLanding.Communication.RequestModel
{
 public class AddFeedbackRequestModel
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Feedback { get; set; }
  }
}
