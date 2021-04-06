﻿using IslandLanding.Models;
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
 public class LoseViewModel:BaseViewModel
  {
    public bool IsLosing { get; set; }
    public GameModel GameModel { get; set; }
    public ICommand MainCommand { get; set; }
    public ICommand TryAginCommand { get; set; }
    public string UserTag { get; set; }
    public string TooLateText { get; set; }
    public LoseViewModel(GameModel game)
    {
      IsLosing = true;
      MainCommand = new Command(MainCommandExcute);
      TryAginCommand = new Command(TryAginCommandExcute);
      GameModel = new GameModel
      {
        MainTime = game.MainTime,
        TakenTime = Math.Abs(game.TakenTime)
      };
      TooLateText = (GameModel.TakenTime > 0) ? " seconds too early" : " seconds too late";
      UserTag = Preferences.Get("userTag", "");
      Device.StartTimer(new TimeSpan(0, 0, 4), () =>
      {
        IsLosing = false;
        return false;
      });
      PageTitle = "LoosePage";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
    }

    private void TryAginCommandExcute(object obj)
    {
      Preferences.Set("levelNumber", 1);
      App.Current.MainPage.Navigation.PushAsync(new GamePage());
    }

    private void MainCommandExcute(object obj)
    {
      Preferences.Set("levelNumber", 1);
      App.Current.MainPage.Navigation.PopToRootAsync();
    }
  }
}
