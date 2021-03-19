using IslandLanding.Enums;
using IslandLanding.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class DifficuilityViewModel : BaseViewModel
  {
    public ICommand BackCommand { get; set; }
    public ICommand DiffcultyCommand { get; set; }
    public DifficuilityViewModel()
    {
      BackCommand = new Command(BackCommandExcute);
      DiffcultyCommand = new Command<string>(EasyCommandExcute);
    }

    private void EasyCommandExcute(string selectedDiffculty)
    {
      if (int.Parse(selectedDiffculty) == (int)Difficulty.Easy)
      {
        Preferences.Set("difficulty", (int)Difficulty.Easy);
      }
      else if (int.Parse(selectedDiffculty) == (int)Difficulty.Medium)
      {
        Preferences.Set("difficulty", (int)Difficulty.Medium);
      }
      else
      {
        Preferences.Set("difficulty", (int)Difficulty.Hard);
      }
      App.Current.MainPage.Navigation.PushAsync(new GamePage());
    }
    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopAsync();
    }
  }
}
