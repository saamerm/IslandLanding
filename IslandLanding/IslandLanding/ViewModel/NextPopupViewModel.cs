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
    public NextPopupViewModel( string difftime)
    {
      Seconds = difftime;
      NextCommand = new Command(NextCommandExcute);
      ButtonText = (Preferences.Get("levelnumber", 1) == 10) ? "Complet" : "Next";

    }

    private void NextCommandExcute(object obj)
    {
      if (Preferences.Get("levelnumber", 1) > 10)
      {
        // will navigate to win page 
      }
      else
      {
        MessagingCenter.Send<NextPopupViewModel>(this, "nextlevel");
      }
      PopupNavigation.Instance.PopAsync();
    }
  }
}
