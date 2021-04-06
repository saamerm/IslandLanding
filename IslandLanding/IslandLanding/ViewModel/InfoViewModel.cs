using MediaManager;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
    public ICommand PlayCommand { get; set; }
    public ICommand CheckusCommand { get; set; }
    public bool IsPlaying { get; set; }
    public string PauseImage { get; set; }
    public InfoViewModel()
    {
      CheckusCommand = new Command(CheckusCommandExcute);
      BackCommand = new Command(BackCommandExcute);
      PlayCommand = new Command(PlayCommandExcute);
      Rules = @"Use this brain training game to improve your time sensibitiliy. When you are ready, you can tap the Jump button and count the assigned time by the captain in your mind. 

You can tap on the Launch Parachute button when you think the time has elapsed. If your time difference is less than 1 second, you can proceed to the next level. Pass 5 levels to submit your average to the scoreboard!

This game is developed by The First Prototype. We specialize in UI design and Mobile App Development, and we love collaborating with others. Background vectors created by pikisuperstar and music by Jorge Hernandez";

      PageTitle = "Info Page";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
      if (Preferences.ContainsKey("playMusic"))
      {
        IsPlaying = Preferences.Get("playMusic", false);
        PauseImage = "volume_up_24px.png";
      }
      else
        IsPlaying = false;
        PauseImage = "volume_off_24px.png";
    }

    private  async void PlayCommandExcute(object obj)
    {
      if (!IsPlaying)
      {
        Preferences.Set("playMusic", true);
        PauseImage = "volume_up_24px.png";
        IsPlaying = true;
        Play();
      }
      else
      {
        PauseImage = "volume_off_24px.png";
        IsPlaying = false;
        Preferences.Set("playMusic", false);
        await CrossMediaManager.Current.Stop();
      }
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
    public static async void Play()
    {
      var audio =  CrossMediaManager.Current;
      await audio.PlayFromAssembly("music.mp3", typeof(BaseViewModel).Assembly);
    }
    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopAsync();

    }
  }
}
