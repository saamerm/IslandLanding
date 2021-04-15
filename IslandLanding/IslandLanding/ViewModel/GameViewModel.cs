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
    public ICommand PlayCommand { get; set; }
    public ObservableCollection<LevelsModel> DotsList { get; set; }
    public bool IsStarting { get; set; }
    public GameModel GameModel { get; set; }
    public List<double> TimeDifferencesList { get; set; }
    public bool IsPlaying { get; set; }
    public string PauseImage { get; set; }
    public GameViewModel()
    {
      ReadyCommand = new Command(ReadyCommandExcute);
      BackCommand = new Command(BackCommandExcute);
      RestartCommand = new Command(RestartCommandExcute);
      LaunchCommand = new Command(LaunchCommandExcute);
      PlayCommand = new Command(PlayCommandExcute);
      TimeDifferencesList = new List<double>();
      DrawLevels();
      MessagingCenter.Subscribe<NextPopupViewModel>(this,"nextLevel", (sender) =>
      {
        IsStarting = false;
        LevelNumber = Preferences.Get("levelNumber", 1) + 1;
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
      if ((Preferences.Get("levelNumber", 1) == 1))
      {
        var difficulitylevel = Preferences.Get("difficulty", Difficulty.Easy.ToString());
        if (difficulitylevel== (Difficulty.Easy.ToString()))
        {
          LevelTime = 5;
        }
        else if(difficulitylevel == Difficulty.Medium.ToString())
        {
          LevelTime = 10;
        }
        else
        {
          LevelTime = 15;
        }
        LevelNumber = 1;
      }
      PageTitle = "GamePage";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
      if (Preferences.ContainsKey("playMusic"))
      {
        IsPlaying = Preferences.Get("playMusic", false);
        PauseImage =(IsPlaying)? "volume_up_24px.png" : "volume_off_24px.png";
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
      var time = DateTime.Now;
      var diffTime = DateTime.Now - StartedTime;
      var scoretime = LevelTime - diffTime.TotalSeconds;
      GameModel = new GameModel
      {
        MainTime = Math.Round(diffTime.TotalSeconds, 2),
        TakenTime = Math.Round(scoretime, 2)
      };
      TimeDifferencesList.Add(Math.Abs(Math.Round(scoretime, 2)));
      var listOfTimeAsJson = JsonConvert.SerializeObject(TimeDifferencesList);
      Preferences.Set("listOfTimeAsJson", listOfTimeAsJson);
      if (scoretime > -1 && scoretime <= 1)
      {
        IsStarting = false;
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
      IsStarting = true;
      Preferences.Set("levelNumber", LevelNumber);
      StartedTime = DateTime.Now;
    }
    /// <summary>
    /// TODO we will use function in future
    /// </summary>
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
    
    }
  }
}
