using IslandLanding.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
 public class HomeViewModel:BaseViewModel
  {
    public ICommand ProfileCommand { get; set; }
    public ICommand LeaderBoardCommand { get; set; }
    public ICommand StartCommand { get; set; }
    public ICommand InfoCommand { get; set; }
    public HomeViewModel()
    {
      ProfileCommand = new Command(ProfileCommandExcute);
      LeaderBoardCommand = new Command(LeaderBoardCommandExcute);
      StartCommand = new Command(StartCommandExcute);
      InfoCommand = new Command(InfoCommandExcute);
      Notify();
    }
    private void Notify()
    {
      if (!Preferences.Get("firstTime", false))
      {
        PopupNavigation.Instance.PushAsync(new WelcomePopupPage());
        Device.StartTimer(new TimeSpan(0, 0, 5), () =>
        {
          if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Any())
          {
            PopupNavigation.Instance.PopAsync();
          }
          return false;
        });
      }
    }
    private void InfoCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new InfoPage());
    }

    private void StartCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new DificullityPage());
    }

    private void LeaderBoardCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new LeaderBoardPage());
    }

    private void ProfileCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new SwitchUserAccountPage());
    }
  }
}
