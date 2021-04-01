using IslandLanding.Enums;
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
  public class DifficuilityViewModel : BaseViewModel
  {
    public ICommand BackCommand { get; set; }
    public ICommand DiffcultyCommand { get; set; }
    public DifficuilityViewModel()
    {
      BackCommand = new Command(BackCommandExcute);
      DiffcultyCommand = new Command<string>(EasyCommandExcute);
      PageTitle = "Choose Difficulity";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
    }

    private void EasyCommandExcute(string selectedDiffculty)
    {
      if (selectedDiffculty == Difficulty.Easy.ToString())
      {
        Preferences.Set("difficulty", Difficulty.Easy.ToString());
      }
      else if (selectedDiffculty == Difficulty.Medium.ToString())
      {
        Preferences.Set("difficulty", Difficulty.Medium.ToString());
      }
      else
      {
        Preferences.Set("difficulty", Difficulty.Hard.ToString());
      }
      App.Current.MainPage.Navigation.PushAsync(new GamePage());
    }
    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopAsync();
    }
  }
}
