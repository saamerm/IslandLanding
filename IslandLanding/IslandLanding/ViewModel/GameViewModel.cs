using IslandLanding.Enums;
using IslandLanding.Models;
using IslandLanding.Views;
using MediaManager;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class GameViewModel : BaseViewModel
  {
    public int LevelTime { get; set; }
    public int LevelNumber { get; set; }
    public double StartedTime { get; set; }
    public ICommand ReadyCommand { get; set; }
    public ICommand BackCommand { get; set; }
    public ICommand RestartCommand { get; set; }
    public ICommand LaunchCommand { get; set; }
    public ICommand PlayCommand { get; set; }
    public ObservableCollection<LevelsModel> DotsList { get; set; }
    public GameModel GameModel { get; set; }
    public List<double> TimeDifferencesList { get; set; }
    public bool IsPlaying { get; set; }
    public bool IsRestarting { get; set; }
    public string PauseImage { get; set; }
    public string LaunchText { get; set; }
    public string JumpButtonText { get; set; }
    public Color JumpButtonBackgroundColor { get; set; }
    public Color JumpButtonBorderColor { get; set; }
    Stopwatch stopwatch { set; get; }
    public GameViewModel()
    {
      ReadyCommand = new Command(ReadyCommandExcute);
      BackCommand = new Command(BackCommandExcute);
      RestartCommand = new Command(RestartCommandExcute);
      LaunchCommand = new Command(LaunchCommandExcute);
      PlayCommand = new Command(PlayCommandExcute);
      TimeDifferencesList = new List<double>();
      stopwatch = new Stopwatch();

      DrawLevels();
      MessagingCenter.Subscribe<NextPopupViewModel>(this, "nextLevel", (sender) =>
       {
         JumpButtonText = "Hold";
         JumpButtonBackgroundColor = Color.FromHex("#E8A24F");
         JumpButtonBorderColor = Color.FromHex("#4F3824");
         LevelNumber = Preferences.Get("levelNumber", 1) + 1;
         LevelTime += 1;
         LaunchText = "Hold for " + LevelTime + " seconds and release to launch parachute";
         IsRestarting = true;
         foreach (var item in DotsList)
         {

           if (item.LevelNumber == LevelNumber.ToString())
           {
             item.IsCompleted = true;
             item.BackgroundColor = Color.FromHex("#C5C5C5");
           }
         }
       });
      MessagingCenter.Subscribe<RestartPopupViewModel>(this, "restartGame", (sender) =>
      {

        LevelNumber = 1;
        CheckLevelTime();
        IsRestarting = false;
        DrawLevels();

      });
      MessagingCenter.Subscribe<LoseViewModel>(this, "tryAgain", (sender) =>
      {
        JumpButtonText = "Hold";
        JumpButtonBackgroundColor = Color.FromHex("#E8A24F");
        JumpButtonBorderColor = Color.FromHex("#4F3824");
        LaunchText = "Hold for " + LevelTime + " seconds and release to launch parachute";

      });
      //this part for check level and add level time according to choosen difficulity
      CheckLevelTime();
      PageTitle = "GamePage";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
      CheckMusicIsPlaying();
    }
    private void CheckMusicIsPlaying()
    {
      if (Preferences.ContainsKey("playMusic"))
      {
        IsPlaying = Preferences.Get("playMusic", false);
        PauseImage = (IsPlaying) ? "volume_up_24px.png" : "volume_off_24px.png";
      }
    }
    private void CheckLevelTime()
    {
      var x = Preferences.Get("levelNumber", 1);
      if ((Preferences.Get("levelNumber", 1) == 1))
      {
        LevelNumber = 1;
        IsRestarting = (LevelNumber == 1) ? false : true;
        var difficulitylevel = Preferences.Get("difficulty", Difficulty.Easy.ToString());
        if (difficulitylevel == (Difficulty.Easy.ToString()))
        {
          LevelTime = 5;
        }
        else if (difficulitylevel == Difficulty.Medium.ToString())
        {
          LevelTime = 10;
        }
        else
        {
          LevelTime = 15;
        }
      }
      else
      {
        LevelNumber = 1;
        LevelTime = 5;
      }
      JumpButtonText = "Hold";
      JumpButtonBackgroundColor = Color.FromHex("#E8A24F");
      JumpButtonBorderColor = Color.FromHex("#4F3824");
      LaunchText = "Hold for " + LevelTime + " seconds and release to launch parachute";
      foreach (var item in DotsList)
      {

        if (item.LevelNumber == LevelNumber.ToString())
        {
          item.IsCompleted = true;
          item.BackgroundColor = Color.FromHex("#C5C5C5");
        }
      }

    }
    private async void PlayCommandExcute(object obj)
    {
      if (!IsPlaying)
      {
        Preferences.Set("playMusic", true);
        PauseImage = "volume_up_24px.png";
        IsPlaying = true;
        var audio = CrossMediaManager.Current;
        await audio.PlayFromAssembly("music.mp3", typeof(BaseViewModel).Assembly);
      }
      else
      {
        PauseImage = "volume_off_24px.png";
        IsPlaying = false;
        Preferences.Set("playMusic", false);
        await CrossMediaManager.Current.Stop();
      }
    }

    private void LaunchCommandExcute(object obj)
    {//between -1 and +1
      stopwatch.Stop();
      var time = stopwatch.Elapsed.TotalSeconds;
      var diffTime = time - StartedTime;
      var scoretime = LevelTime - diffTime;
      GameModel = new GameModel
      {
        LevelTime = LevelTime,
        MainTime = Math.Round(diffTime, 2),
        TakenTime = Math.Round(scoretime, 2)
      };
      TimeDifferencesList.Add(Math.Abs(Math.Round(scoretime, 2)));
      var listOfTimeAsJson = JsonConvert.SerializeObject(TimeDifferencesList);
      Preferences.Set("listOfTimeAsJson", listOfTimeAsJson);
      if (scoretime > -1 && scoretime <= 1)
      {
        PopupNavigation.Instance.PushAsync(new NextPopupPage(GameModel));
      }
      else
      {
        // will go to lose page
        App.Current.MainPage.Navigation.PushAsync(new LosePage(GameModel));
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
      LaunchText = "Release in " + LevelTime + " seconds to launch parachute";
      JumpButtonText = "Release";
      Preferences.Set("levelNumber", LevelNumber);
      JumpButtonBackgroundColor = Color.FromHex("#4F3824");
      JumpButtonBorderColor = Color.FromHex("#E8A24F");
      stopwatch.Start();
      StartedTime = stopwatch.Elapsed.TotalSeconds;

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

    }
  }
}
