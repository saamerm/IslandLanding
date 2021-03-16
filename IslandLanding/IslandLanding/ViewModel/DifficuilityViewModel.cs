using IslandLanding.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class DifficuilityViewModel : BaseViewModel
  {
    public ICommand BackCommand { get; set; }
    public ICommand EasyCommand { get; set; }
    public DifficuilityViewModel()
    {
      BackCommand = new Command(BackCommandExcute);
      EasyCommand = new Command(EasyCommandExcute);
    }

    private void EasyCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new GamePage());
    }

    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopAsync();
    }
  }
}
