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
    public HomeViewModel()
    {
      ProfileCommand = new Command(ProfileCommandExcute);
    }
    private void ProfileCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new SwitchUserAccountPage());
    }
  }
}
