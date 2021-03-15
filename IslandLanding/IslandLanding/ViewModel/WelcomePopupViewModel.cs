using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace IslandLanding.ViewModel
{
 public class WelcomePopupViewModel:BaseViewModel
  {
    string _gamerTag;
    public string GamerTag
    {
      get => _gamerTag;
      set => SetProperty(ref _gamerTag, value);
    }
    public WelcomePopupViewModel()
    {
      GamerTag = Preferences.Get("userTag", "");
    }
  }
}
