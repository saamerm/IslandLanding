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
  public class RestartPopupViewModel : BaseViewModel
  {
    public ICommand YesCommand { get; set; }
    public ICommand NoCommand { get; set; }
    public RestartPopupViewModel()
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
      Preferences.Set("levelNumber", 1);
      // App.Current.MainPage.Navigation.PushAsync(new GamePage());
      MessagingCenter.Send<RestartPopupViewModel>(this, "restartGame");
      PopupNavigation.Instance.PopAsync();
    }
  }
}
