using IslandLanding.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IslandLanding.Models
{
 public class LeaderBoardModel:BaseViewModel
  {
    public int Rank { get; set; }
    public string Name { get; set; }
    public double Score { get; set; }
    public Color BackgroundColor { get; set; }
  }
}
