using IslandLanding.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
 public class ExitPopupViewModel:BaseViewModel
  {
    public ICommand YesCommand { get; set; }
    public ICommand NoCommand { get; set; }
    public ExitPopupViewModel()
    {
      YesCommand = new Command(YesCommandExcute);
      NoCommand = new Command(NoCommandExcute);
    }

    private void NoCommandExcute(object obj)
    {
      PopupNavigation.Instance.PopAsync();
    }

    private void YesCommandExcute(object obj)
    {
      //throw new NotImplementedException();
      var isPlaying = Preferences.Get("playMusic", false);
      Preferences.Set("levelNumber", 1);
      MessagingCenter.Send<ExitPopupViewModel, bool>(this, "isPlaying", isPlaying);
      App.Current.MainPage.Navigation.PopToRootAsync();
      PopupNavigation.Instance.PopAsync();
    }
  }
}
