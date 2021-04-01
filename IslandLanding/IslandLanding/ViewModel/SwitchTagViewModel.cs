using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class SwitchTagViewModel : BaseViewModel
  {
    public ICommand BackCommand { get; set; }
    private string _userTag;
    public string UserTag
    {
      get => _userTag;
      set => SetProperty(ref _userTag, value);
    }
    public ICommand SaveCommand { get; set; }
    public SwitchTagViewModel()
    {
      BackCommand = new Command(BackCommandExcute);
      SaveCommand = new Command(SaveCommandExcute);
      PageTitle = "Switch Tag";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
    }

    private void SaveCommandExcute(object obj)
    {
      Preferences.Set("userTag", UserTag);
      App.Current.MainPage.Navigation.PopAsync();
    }

    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopAsync();

    }
  }
}
