using IslandLanding.Models;
using IslandLanding.Views;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class LoseViewModel : BaseViewModel
  {
    public bool IsLosing { get; set; }
    public GameModel GameModel { get; set; }
    public ICommand MainCommand { get; set; }
    public ICommand TryAginCommand { get; set; }
    public string UserTag { get; set; }
    private string _tooLateTex;
    public string TooLateText
    {
      get => _tooLateTex;
      set => SetProperty(ref _tooLateTex, value);
    }
    public string FinalLoseText { get; set; }
    public bool IsPlaying { get; set; }
    public LoseViewModel(GameModel game)
    {
      IsLosing = true;
      MainCommand = new Command(MainCommandExcute);
      TryAginCommand = new Command(TryAginCommandExcute);
      GameModel = new GameModel
      {
        LevelTime = game.LevelTime,
        MainTime = game.MainTime,
        TakenTime = Math.Abs(game.TakenTime)
      };
      IsPlaying = true;
      TooLateText = (GameModel.MainTime < GameModel.LevelTime) ? " seconds too early" : " seconds too late";
      UserTag = Preferences.Get("userTag", "");
      Device.StartTimer(new TimeSpan(0, 0, 4), () =>
      {
        IsLosing = false;
        return false;
      });
      PageTitle = "LoosePage";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
      FinalLoseText = "It took you " + GameModel.MainTime + " seconds, which is " + GameModel.TakenTime + TooLateText;
    }

    private void TryAginCommandExcute(object obj)
    {
      Preferences.Set("levelNumber", 1);
      MessagingCenter.Send<LoseViewModel>(this, "tryAgain");
      IsPlaying = false;
      App.Current.MainPage.Navigation.PopAsync();
    }

    private void MainCommandExcute(object obj)
    {
      Preferences.Set("levelNumber", 1);
      IsPlaying = false;
      App.Current.MainPage.Navigation.PopToRootAsync();
    }
  }
}
