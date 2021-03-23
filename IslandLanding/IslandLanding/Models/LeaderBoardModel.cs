using IslandLanding.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IslandLanding.Models
{
 public class LeaderBoardModel:BaseViewModel
  {
    public string Rank { get; set; }
    public string Name { get; set; }
    public string Time { get; set; }
    public Color BackgroundColor { get; set; }
  }
}
