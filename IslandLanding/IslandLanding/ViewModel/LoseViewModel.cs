﻿using IslandLanding.Models;
using IslandLanding.Views;
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
    public LoseViewModel(GameModel game)
    {
      IsLosing = true;
      MainCommand = new Command(MainCommandExcute);
      TryAginCommand = new Command(TryAginCommandExcute);
      GameModel = game;
      UserTag = Preferences.Get("userTag", "");
      Device.StartTimer(new TimeSpan(0, 0, 4), () =>
      {
        IsLosing = false;
        return false;
      });
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