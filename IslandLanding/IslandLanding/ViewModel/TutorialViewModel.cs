using IslandLanding.Views;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
 public class TutorialViewModel:BaseViewModel
  {
    public ICommand CurrentItemCommand { get; set; }
    public ICommand SkipCommand { get; set; }
    public ICommand NextCommand { get; set; }
    public ICommand PositionChangedCommand { get; set; }
    private ObservableCollection<string> _tutorialList;
    public ObservableCollection<string> TutorialList
    {
      get => _tutorialList;
      set => SetProperty(ref _tutorialList, value);
    }
    int _postion;
    public int Postion
    {
      get => _postion;
      set => SetProperty(ref _postion, value);
    }
    public int PreviousPosition { get; set; }
    public int CurrentPosition { get; set; }
    string _buttonText;
    public string ButtonText
    {
      get => _buttonText;
      set => SetProperty(ref _buttonText, value);
    }

    public TutorialViewModel()
    {
      TutorialList = new ObservableCollection<string>();
      NextCommand = new Command(NextCommandExute);
      Postion = 0;
      ButtonText = "Next";
      LoadData();
      PositionChangedCommand = new Command<int>(PositionChanged);
      PageTitle = "Tutorial Page";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
    }

    private void NextCommandExute(object obj)
    {
      PreviousPosition = CurrentPosition;
      //CurrentPosition = position;
      if (CurrentPosition < TutorialList.Count - 1)
      {
        CurrentPosition++;
      }

      if (CurrentPosition == TutorialList.Count - 1)
      {
        ButtonText = "Let’s Play!";
      }
    }

    private void PositionChanged(int position)
    {
      PreviousPosition = CurrentPosition;
      CurrentPosition = position;
      ButtonText = "Next";
      if (CurrentPosition == TutorialList.Count - 1)
      {
        ButtonText = "Let’s Play!";
        Preferences.Set("firstTime", true);
        App.Current.MainPage = new NavigationPage(new HomePage());
      }
    }
    private void LoadData()
    {
      TutorialList.Add("Hi mike,You are a pessenger of a flight,your flight is undergoing a severe crash");
      TutorialList.Add("Listen to the captain’s instructions and launch your parachute in the given time");
      TutorialList.Add("If your time difference is greater than 1 second, you will land on a lake while others will land on the island");
    }
  }
}
