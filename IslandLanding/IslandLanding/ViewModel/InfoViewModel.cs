﻿using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class InfoViewModel : BaseViewModel
  {
    string _rules;
    public string Rules
    {
      get => _rules;
      set => SetProperty(ref _rules, value);
    }
    public ICommand BackCommand { get; set; }
    public ICommand CheckusCommand { get; set; }
    public InfoViewModel()
    {
      CheckusCommand = new Command(CheckusCommandExcute);
      BackCommand = new Command(BackCommandExcute);
      Rules = @"Use this brain training game to improve your time sensibitiliy. When you are ready, you can tap the Ready button and count the assigned time by the captain in your mind. 

You can tap on the Launch Parachute button when you think the time has elapsed. If your time difference is less than 1 second, you can proceed to the next level. Pass 5 levels to submit your average to the scoreboard!

This game is developed by The First Prototype.We specialize in UI design and Mobile App Development, and we love collaborating with others. Background vectors created by pikisuperstar and music by Jorge Hernandez";

      PageTitle = "Info Page";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
    }

    private async void CheckusCommandExcute(object obj)
    {
      try
      {
        await Browser.OpenAsync("https://thefirstprototype.com/", BrowserLaunchMode.SystemPreferred);
      }
      catch (Exception ex)
      {
        // An unexpected error occured. No browser may be installed on the device.
      }
    }

    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopAsync();

    }
  }
}
