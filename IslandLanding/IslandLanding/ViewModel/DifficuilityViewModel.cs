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
    public DifficuilityViewModel()
    {
      BackCommand = new Command(BackCommandExcute);
    }

    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopAsync();
    }
  }
}
