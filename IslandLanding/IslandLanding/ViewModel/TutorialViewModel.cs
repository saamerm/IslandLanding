using IslandLanding.Views;
using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
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
    public ICommand PlayCommand { get; set; }
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
    public bool IsPlaying { get; set; }
    public bool IsNext { get; set; }
    public TutorialViewModel()
    {
      TutorialList = new ObservableCollection<string>();
      NextCommand = new Command(NextCommandExute);
      PlayCommand = new Command(PlayCommandExcute);
      Postion = 0;
      ButtonText = "Next";
      LoadData();
      PositionChangedCommand = new Command<int>(PositionChanged);
      PageTitle = "Tutorial Page";
      IsNext = true;
      IsPlaying = false;
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
    }

    private void PlayCommandExcute(object obj)
    {
      App.Current.MainPage = new NavigationPage(new HomePage());
    }

    private void NextCommandExute(object obj)
    {
      PreviousPosition = CurrentPosition;
      //CurrentPosition = position;
      if (CurrentPosition+1 != TutorialList.Count )
      {
        CurrentPosition++;
      }
    }

    private void PositionChanged(int position)
    {
      PreviousPosition = CurrentPosition;
      CurrentPosition = position;
      ButtonText = "Next";
      Preferences.Set("firstTime", true);
     
      if (CurrentPosition == TutorialList.Count-1)
      {
        ButtonText = "Let’s Play!";
        IsNext = false;
        IsPlaying = true;
        // App.Current.MainPage.Navigation.PopToRootAsync();
      }
    }
    private void LoadData()
    {
      TutorialList.Add("Hi "+ Preferences.Get("userTag", "")+ ", You’re a passenger in a flight that’s going to crash. Your flight is undergoing severe turbulence");
      TutorialList.Add("Listen to the captain’s instructions as the time disappears after you jump. Launch your parachute in the given time");
      TutorialList.Add("Launch your parachute within 1 second  of the target, so that you will land on the island and not get eaten up by the sharks");
    }
  }
}
