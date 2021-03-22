using IslandLanding.Enums;
using IslandLanding.Models;
using IslandLanding.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class GameViewModel : BaseViewModel
  {
    public string Seconds { get; set; }
    public int LevelTime { get; set; }
    public int LevelNumber { get; set; }
    public DateTime StartedTime { get; set; }
    public ICommand ReadyCommand { get; set; }
    public ICommand BackCommand { get; set; }
    public ICommand RestartCommand { get; set; }
    public ICommand LaunchCommand { get; set; }
    public ObservableCollection<LevelsModel> DotsList { get; set; }
    public bool IsStarting { get; set; }
    public GameModel gameModel { get; set; }
    public List<double> TimeDifferencesList { get; set; }
    public GameViewModel()
    {
      ReadyCommand = new Command(ReadyCommandExcute);
      BackCommand = new Command(BackCommandExcute);
   
      RestartCommand = new Command(RestartCommandExcute);
      LaunchCommand = new Command(LaunchCommandExcute);
      TimeDifferencesList = new List<double>();
      DrawLevels();
      MessagingCenter.Subscribe<NextPopupViewModel>(this,"nextlevel", (sender) =>
      {
        IsStarting = false;
        LevelNumber = Preferences.Get("levelnumber", 1) + 1;
        LevelTime += 1;
        foreach (var item in DotsList)
        {
          
          if(item.LevelNumber==LevelNumber.ToString())
          {
            item.IsCompleted = true;
            item.BackgroundColor = Color.FromHex("#C5C5C5");
          }
          
        }
      });
      //this part for check level and add level time according to choosen difficulity
      if ((Preferences.Get("levelnumber", 1) == 1))
      {
        var difficulitylevel = Preferences.Get("difficulty", (int)Difficulty.Easy);
        if (difficulitylevel== (int)Difficulty.Easy)
        {
          LevelTime = 5;
        }
        else if(difficulitylevel == (int)Difficulty.Medium)
        {
          LevelTime = 25;
        }
        else
        {
          LevelTime = 45;
        }
       
        LevelNumber = 1;

      }
    }

    private void LaunchCommandExcute(object obj)
    {//between -1 and +1
      var time = DateTime.Now;
      var diffTime = DateTime.Now - StartedTime;
      var scoretime = LevelTime - diffTime.TotalSeconds;
      gameModel = new GameModel
      {
        MainTime = diffTime.TotalSeconds.ToString(),
        TakenTime = scoretime.ToString()
      };
      TimeDifferencesList.Add(scoretime);
      var listOfTimeAsJson = JsonConvert.SerializeObject(TimeDifferencesList);
      Preferences.Set(listOfTimeAsJson, "");
      if (scoretime > -1 && scoretime <= 1)
      {
        PopupNavigation.Instance.PushAsync(new NextPopupPage(scoretime.ToString()));

      }
      else
      {
        // will go to lose page

        App.Current.MainPage.Navigation.PushAsync(new LosePage(gameModel));
       
      }

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
      // store time .now in varaible 
      // when press launch compare time.now with varaible 
      // then sub the 2 times 
      //CalculateTimeRemaining();
      Preferences.Set("levelnumber", LevelNumber);
      StartedTime = DateTime.Now;
    }

    public void CalculateTimeRemaining()
    {


      //DateTime timeExpired = DateTime.Now.AddSeconds(8);
      //Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 1), () =>
      //{
      //  var timespan = timeExpired - DateTime.Now;
      //  if (timespan.TotalSeconds > 1)
      //  { 
      //    Seconds = "in " + timespan.Seconds.ToString() + " s";

      //    return true;
      //  }
      //  else
      //  {

      //    Seconds = "0:0:0";
      //    return false;
      //  }
      //});


    }
    private void DrawLevels()
    {
      // here we will make list to draw 10 levels dots and make first 1 is selected 
      // change its background color slected grey unselected black
      //also i will hide design when press ready and show design for foucus on time and animatiom and stop button 
      DotsList = new ObservableCollection<LevelsModel>();
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "1",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
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
