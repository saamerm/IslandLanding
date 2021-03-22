using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
 public class NextPopupViewModel:BaseViewModel
  {
    public string Seconds { get; set; }
    public string ButtonText { get; set; }
    public ICommand NextCommand { get; set; }
    public NextPopupViewModel( string diffTime)
    {
      Seconds = diffTime;
      NextCommand = new Command(NextCommandExcute);
      ButtonText = (Preferences.Get("levelNumber", 1) == 10) ? "Complete" : "Next";

    }

    private void NextCommandExcute(object obj)
    {
      if (Preferences.Get("levelNumber", 1) > 10)
      {
        // will navigate to win page 
      }
      else
      {
        MessagingCenter.Send<NextPopupViewModel>(this, "nextLevel");
      }
      PopupNavigation.Instance.PopAsync();
    }
  }
}
