using IslandLanding.Controls;
using IslandLanding.Views;
using Microsoft.AppCenter.Analytics;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class GamerTagViewModel : BaseViewModel
  {
    private string _userTag;
    public string UserTag
    {
      get => _userTag;
      set => SetProperty(ref _userTag, value);
    }
    private string _popupText;
    public string PopupText
    {
      get => _popupText;
      set => SetProperty(ref _popupText, value);
    }
    public ICommand SaveCommand { get; set; }
    public GamerTagViewModel()
    {
      SaveCommand = new Command(SaveCommandExcute);
      PageTitle = "GamerTag Page";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
    }

    private void SaveCommandExcute(object obj)
    {
      if (!string.IsNullOrEmpty(UserTag))
      {
        Preferences.Set("userTag", UserTag);
        App.Current.MainPage.Navigation.PushAsync(new TutorialPage());
      }
      else
      {
        PopupText = "Gamer tag cannot be empty";
        var dialog = new SimplePopupTemplate();
        dialog.BindingContext = this;
        PopupNavigation.Instance.PushAsync(dialog);
      }
    }
  }
}
