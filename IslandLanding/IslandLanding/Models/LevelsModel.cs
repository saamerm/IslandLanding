using IslandLanding.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IslandLanding.Models
{
 public class LevelsModel:BaseViewModel
  {
    public bool IsCompleted { get; set; }
    public string LevelNumber { get; set; }
    public Color BackgroundColor { get; set; }
  }
}
