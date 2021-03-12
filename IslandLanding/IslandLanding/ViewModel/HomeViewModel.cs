using IslandLanding.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
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
