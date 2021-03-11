using IslandLanding.Views;
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
    public ICommand SaveCommand { get; set; }
    public GamerTagViewModel()
    {
      SaveCommand = new Command(SaveCommandExcute);
    }

    private void SaveCommandExcute(object obj)
    {
      Preferences.Set("userTag", UserTag);
      App.Current.MainPage.Navigation.PushModalAsync(new TutorialPage());
    }
  }
}
