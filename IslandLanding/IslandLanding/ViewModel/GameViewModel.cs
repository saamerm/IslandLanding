using IslandLanding.Models;
using IslandLanding.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class GameViewModel:BaseViewModel
  {
    public string Seconds { get; set; }
    public ICommand ReadyCommand { get; set; }
    public ICommand BackCommand { get; set; }
    public ICommand RestartCommand { get; set; }
    public ObservableCollection<LevelsModel> DotsList { get; set; }
    public bool IsStarting{ get; set;}
    public GameViewModel()
    {
      ReadyCommand = new Command(ReadyCommandExcute);
      BackCommand = new Command(BackCommandExcute);
      DotsList = new ObservableCollection<LevelsModel>();
      RestartCommand = new Command(RestartCommandExcute);
      DrawLevels();
    }

    private void RestartCommandExcute(object obj)
    {
      PopupNavigation.Instance.PushAsync(new RestartPopupPage());
    }

    private void BackCommandExcute(object obj)
    {
      PopupNavigation.Instance.PushAsync(new ExitPopupPage());

    }

    private void ReadyCommandExcute(object obj)
    {
      IsStarting = true;
      CalculateTimeRemaining();
    }

    public void CalculateTimeRemaining()
    {


      DateTime timeExpired = DateTime.Now.AddSeconds(8);
      Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 1), () =>
      {
        var timespan = timeExpired - DateTime.Now;
        if (timespan.TotalSeconds > 0)
        {
          Seconds = "in " + timespan.Seconds.ToString() + " s";
          return true;
        }
        else
        {
          
          Seconds = "0:0:0";
          return false;
        }
      });


    }
    private void DrawLevels()
    {
      // here we will make list to draw 10 levels dots and make first 1 is selected 
      // change its background color slected grey unselected black
      //also i will hide design when press ready and show design for foucus on time and animatiom and stop button 
      DotsList.Add(new LevelsModel
      {
        LevelNumber="1",
        IsCompleted=true,
        BackgroundColor=Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "2",
        IsCompleted = false,
        BackgroundColor = Color.Black
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "3",
        IsCompleted = false,
        BackgroundColor = Color.Black
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "4",
        IsCompleted = false,
        BackgroundColor = Color.Black
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "5",
        IsCompleted = false,
        BackgroundColor = Color.Black
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "6",
        IsCompleted = false,
        BackgroundColor = Color.Black
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "7",
        IsCompleted = false,
        BackgroundColor = Color.Black
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "8",
        IsCompleted = false,
        BackgroundColor = Color.Black
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "9",
        IsCompleted = false,
        BackgroundColor = Color.Black
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "10",
        IsCompleted = false,
        BackgroundColor = Color.Black
      });
    }
  }
}
