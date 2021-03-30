using IslandLanding.Communication.Services;
using IslandLanding.Models;
using Microsoft.AppCenter.Crashes;
using Sharpnado.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class LeaderBoardViewModel : BaseViewModel
  {
    public ICommand BackCommand { get; set; }
    public string Difficulity { get; set; }
    public ObservableCollection<LeaderBoardModel> BoardList { get; set; }
    public ICommand TabSelectedCommand { get; set; }
    private int _selectedViewModelIndex = 0;
    public int SelectedViewModelIndex
    {
      get => _selectedViewModelIndex;
      set => SetProperty(ref _selectedViewModelIndex, value);
    }
    public string BestScore { get; set; }
    public LeaderBoardViewModel()
    {
      BackCommand = new Command(BackCommandExcute);
      BoardList = new ObservableCollection<LeaderBoardModel>();
      TabSelectedCommand = new Command(TabSelectedCommandExcute);
      BestScore = "Your best time is " + Preferences.Get("playerScore", "") + "s";
     GetBoardData("Easy");
    }
    private void TabSelectedCommandExcute(object obj)
    {
      var index = obj as TabHostView;
      SelectedViewModelIndex = index.SelectedIndex;
      if (SelectedViewModelIndex == 0)
      {
        Difficulity = "Easy";
      }
      else if(SelectedViewModelIndex == 1)
      {
        Difficulity = "Medium";
      }
      else
      {
        Difficulity = "Hard";
      }
      BoardList.Clear();
      GetBoardData(Difficulity);
    }
    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopToRootAsync();
    }
    private async void GetBoardData(string difficulity)
    {
      var getLeaderBoardService = new GetLeaderBoardService();
      try
      {
        if (!IsBusy)
        {
          IsBusy = true;
          var ListData = await getLeaderBoardService.GetBoard(difficulity);
          BoardList = new ObservableCollection<LeaderBoardModel>(ListData);
          if (BoardList.Count > 0)
          {
            for (int i = 0; i < BoardList.Count; i++)
            {
              if (i % 2 != 0)
              {
                BoardList[i].BackgroundColor = Color.FromHex("#C4C4C4");
              }
            }
          }
          IsBusy = false;
        }
      }
      catch (Exception e)
      {
        IsBusy = false;
        Crashes.TrackError(e);
      }
      finally
      {
        IsBusy = false;
      }
    }
  }
}
