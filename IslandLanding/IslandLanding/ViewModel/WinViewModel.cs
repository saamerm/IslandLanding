using IslandLanding.Models;
using IslandLanding.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
 public class WinViewModel:BaseViewModel
  {
    public bool IsWinning { get; set; }
    public string UserTag { get; set;}
    public double AverageTime { get; set; }
    public ICommand MainCommand { get; set; }
    public ICommand TryAginCommand { get; set; }
    public ObservableCollection<LevelsModel> DotsList { get; set; }
    public WinViewModel()
    {
      MainCommand = new Command(MainCommandExcute);
      TryAginCommand = new Command(TryAginCommandExcute);
      IsWinning = true;
      UserTag = Preferences.Get("userTag", "");
      var diffTimeListJson = Preferences.Get("listOfTimeAsJson", "");
      var DiffList = JsonConvert.DeserializeObject<List<double>>(diffTimeListJson);
      AverageTime = DiffList.Sum()/DiffList.Count;
      Preferences.Set("playerScore", AverageTime);
      Device.StartTimer(new TimeSpan(0, 0, 3), () =>
      {
        IsWinning = false;

        return false;
      });
      DrawLevels();
    }
    private void TryAginCommandExcute(object obj)
    {
      Preferences.Set("levelNumber", 1);
      App.Current.MainPage.Navigation.PushAsync(new GamePage());
    }

    private void MainCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopToRootAsync();
    }
    private void DrawLevels()
    {
     
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
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "3",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "4",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "5",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "6",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "7",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "8",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "9",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "10",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
    }
  }
}
