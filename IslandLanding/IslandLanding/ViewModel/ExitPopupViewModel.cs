using IslandLanding.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
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
      App.Current.MainPage.Navigation.PopAsync();
      PopupNavigation.Instance.PopAsync();
    }
  }
}
