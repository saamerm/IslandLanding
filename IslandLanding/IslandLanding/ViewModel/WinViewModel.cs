﻿using IslandLanding.Communication.RequestModel;
using IslandLanding.Communication.Services;
using IslandLanding.Communication.Services.AddScore;
using IslandLanding.Enums;
using IslandLanding.Models;
using IslandLanding.Views;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.StoreReview;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
 public class WinViewModel:BaseViewModel
  {
    public bool IsWinning { get; set; }
    public bool IsTop { get; set; }
    public string UserTag { get; set;}
    public double AverageTime { get; set; }
    public string ShowAverageTime { get; set; }
    public string ShowText { get; set; }
    public ICommand MainCommand { get; set; }
    public ICommand TryAginCommand { get; set; }
    public ICommand YesCommand { get; set; }
    public ICommand NoCommand { get; set; }
    public ObservableCollection<LevelsModel> DotsList { get; set; }
    public int NumberOfVisit { get; set; }
    public WinViewModel()
    {
      MainCommand = new Command(MainCommandExcute);
      TryAginCommand = new Command(TryAginCommandExcute);
      YesCommand = new Command(YesCommandExcute);
      NoCommand = new Command(NoCommandExcute);
      IsWinning = true;
      UserTag = Preferences.Get("userTag", "");
      PageTitle = "Win Page";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
      Device.StartTimer(new TimeSpan(0, 0, 3), () =>
      {
        IsWinning = false;
        CheckHighScore();
        return false;
      });

      if (Preferences.ContainsKey("numberOfVisit"))
      {
        NumberOfVisit = Preferences.Get("numberOfVisit", NumberOfVisit);
      }
      else
      {
        NumberOfVisit = 1;
      }
      DrawLevels();
     
    }
    private void OpenAppReviewPopup()
    {
      var dialog = new AppReviewPopupPage();
      dialog.BindingContext = this;
      PopupNavigation.Instance.PushAsync(dialog);
    }
    private void CheckNumberOfVisit()
    {
      if (NumberOfVisit == 1||NumberOfVisit==3 || NumberOfVisit == 10)
        {
          OpenAppReviewPopup();
        }
      NumberOfVisit++;
      Preferences.Set("numberOfVisit", NumberOfVisit);
    }
    private void NoCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new FeedBackPage());
    }

    private async void YesCommandExcute(object obj)
    {
      await CrossStoreReview.Current.RequestReview(false);
    }

    private void CheckHighScore()
    {
      ShowText = "Your average is ";
      var diffTimeListJson = Preferences.Get("listOfTimeAsJson", "");
      var DiffList = JsonConvert.DeserializeObject<List<double>>(diffTimeListJson);
      AverageTime = Math.Abs(DiffList.Sum() / DiffList.Count);
      ShowAverageTime = AverageTime + " seconds";
      if (Preferences.ContainsKey("playerScore"))
      {
        var prevousScore=Preferences.Get("playerScore", 0.0);
        if(AverageTime<prevousScore)
        {
          Preferences.Set("playerScore", AverageTime);
          IsTop = true;
          PopupNavigation.Instance.PushAsync(new WinPopupPage());
          Device.StartTimer(new TimeSpan(0, 0, 10), () =>
          {
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Any())
            {
              PopupNavigation.Instance.PopAsync();
            }
            return false;
          });
         
        }
      }
      else
      {
        Preferences.Set("playerScore", AverageTime);
      }
        PostScore();
    }
    private async void PostScore()
    {
      try
      {
        if (!IsBusy)
        {
          var addScoreService = new AddScoreService();
          var requestModel = new AddScoreRequestModel { Name = UserTag, Score = AverageTime.ToString(), Difficuilty = Preferences.Get("difficulty", Difficulty.Easy.ToString()) };
          var response = await addScoreService.AddScore(requestModel);
          if (response.Status != null)
          {
            Device.StartTimer(new TimeSpan(0, 0, 6), () =>
            {
              ShowText = "You are now ranked ";
              ShowAverageTime = "NO." + response.Rank;
              CheckNumberOfVisit();
              return false;
            });
          }
        }
      }
      catch(Exception ex)
      {
        IsBusy = false;
        Crashes.TrackError(ex);
      }
      finally
      {
        IsBusy = false;
      }
    }
    //TODO in the futur will use it if we want to get ranks 
    //private async void GetRank()
    //{
    //  var getLeaderBoardService = new GetLeaderBoardService();
    //  var ListData = await getLeaderBoardService.GetBoard();
    //  if(ListData.Count>0)
    //  {
    //    try
    //    {
    //      var rank = ListData.Where(x => x.Name == UserTag).ToList();
    //      if (rank.Count>0)
    //      {

    //        ShowText = "You are now ranked ";
    //        ShowAverageTime = "NO." + rank.FirstOrDefault().Rank;
    //      }
    //    }
    //    catch (Exception e)
    //    {
    //      //TODO Handle exception
    //    }
    //  }
    //}
    private void TryAginCommandExcute(object obj)
    {
      Preferences.Set("levelNumber", 1);
      App.Current.MainPage.Navigation.PushAsync(new GamePage());
    }

    private void MainCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopToRootAsync();
    }
    private void DrawLevels()
    {
     
      DotsList = new ObservableCollection<LevelsModel>();
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "1",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "2",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "3",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "4",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "5",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "6",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "7",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "8",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "9",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
      DotsList.Add(new LevelsModel
      {
        LevelNumber = "10",
        IsCompleted = true,
        BackgroundColor = Color.FromHex("#C5C5C5")
      });
    }
  }
}
