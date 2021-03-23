using IslandLanding.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
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
    public LeaderBoardViewModel()
    {
      BackCommand = new Command(BackCommandExcute);
      BoardList = new ObservableCollection<LeaderBoardModel>();
      GetBoardData();
    }
    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopToRootAsync();
    }
    private void GetBoardData()
    {
      BoardList.Add(new LeaderBoardModel { Name = "amira", Rank = "1", Time = "0.01",BackgroundColor=Color.FromHex("#E4E4E4") });
      BoardList.Add(new LeaderBoardModel { Name = "mero", Rank = "2", Time = "0.1", BackgroundColor = Color.FromHex("#C4C4C4") });
      BoardList.Add(new LeaderBoardModel { Name = "sam", Rank = "3", Time = "0.2", BackgroundColor = Color.FromHex("#E4E4E4") });
      BoardList.Add(new LeaderBoardModel { Name = "mero", Rank = "4", Time = "0.3", BackgroundColor = Color.FromHex("#C4C4C4") });
    }
  }
}
