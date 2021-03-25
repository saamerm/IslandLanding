using IslandLanding.Communication.Services;
using IslandLanding.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
 public class LeaderBoardViewModel:BaseViewModel
  {
    public ICommand BackCommand { get; set; }
    public ObservableCollection<LeaderBoardModel> BoardList { get; set; }
    private int _selectedViewModelIndex = 0;
    public int SelectedViewModelIndex
    {
      get => _selectedViewModelIndex;
      set => SetProperty(ref _selectedViewModelIndex, value);
    }
    public bool IsBusy { get; set; }
    public string BestScore { get; set; }
    public LeaderBoardViewModel()
    {
      BackCommand = new Command(BackCommandExcute);
      BoardList = new ObservableCollection<LeaderBoardModel>();
      BestScore = "Your best time is "+ Preferences.Get("playerScore", "")+"s";
      GetBoardData();
    }
    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopToRootAsync();
    }
    private async void GetBoardData()
    {
      var getLeaderBoardService = new GetLeaderBoardService();
      try
      {
        if (!IsBusy)
        {
          IsBusy = true;
          var ListData = await getLeaderBoardService.GetBoard();
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
      catch (Exception)
      {
        IsBusy = false;
        
      }
      finally
      {
        IsBusy = false;
      }
    }
  }
}
