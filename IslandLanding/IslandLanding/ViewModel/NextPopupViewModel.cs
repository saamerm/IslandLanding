using IslandLanding.Models;
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
  public class NextPopupViewModel : BaseViewModel
  {
    public string Seconds { get; set; }
    public string ButtonText { get; set; }
    public ICommand NextCommand { get; set; }
    public GameModel GameModel { get; set; }
    public NextPopupViewModel(GameModel _gamemodel)
    {
      GameModel = _gamemodel;
      ShowTips();
      NextCommand = new Command(NextCommandExcute);
      ButtonText = (Preferences.Get("levelNumber", 1) == 5) ? "Complete" : "Next Level";

    }
    private void ShowTips()
    {
      if (GameModel.TakenTime > 0.5 || GameModel.TakenTime < -0.5)
      {
        Seconds = GameModel.MainTime + "  seconds, you barely missed the sharks!";
      }
      else if (GameModel.TakenTime <= 0.1 || GameModel.TakenTime >= -0.1)
      {
        Seconds = GameModel.MainTime + " seconds, your accuracy is amazing, you must be a skydiver!";
      }
      else if (GameModel.TakenTime > 0.1|| GameModel.TakenTime < -0.1)
      {
        Seconds = GameModel.MainTime + " seconds, you made it on the island!";
      }
    }
    private void NextCommandExcute(object obj)
    {
      if (Preferences.Get("levelNumber", 1) == 5)
      {
        App.Current.MainPage.Navigation.PushAsync(new WinPage());
      }
      else
      {
        MessagingCenter.Send<NextPopupViewModel>(this, "nextLevel");
      }
      PopupNavigation.Instance.PopAsync();
    }
  }
}
