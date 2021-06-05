using IslandLanding.Communication.RequestModel;
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
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
    public class WinViewModel : BaseViewModel
    {
        //public bool IsWinning { get; set; }
        public bool IsTop { get; set; }
        public string UserTag { get; set; }
        private bool _isViewBusy;
        public bool IsViewBusy
        {
            get => _isViewBusy;
            set => SetProperty(ref _isViewBusy, value);
        }
        public double AverageTime { get; set; }
        public string ShowAverageTime { get; set; }
        public string ShowText { get; set; }
        public ICommand MainCommand { get; set; }
        public ICommand TryAginCommand { get; set; }
        public ICommand YesCommand { get; set; }
        public ICommand NoCommand { get; set; }
        public string WinText { get; set; }
        public ObservableCollection<LevelsModel> DotsList { get; set; }
        public int NumberOfVisit { get; set; }
        public WinViewModel()
        {
            MainCommand = new Command(MainCommandExcute);
            TryAginCommand = new Command(TryAginCommandExcute);
            YesCommand = new Command(YesCommandExcute);
            NoCommand = new Command(NoCommandExcute);
            // IsWinning = true;

            //Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            //   {
            //       CheckHighScore();
            //       return false;
            //   });

            if (Preferences.ContainsKey("numberOfVisit"))
            {
                NumberOfVisit = Preferences.Get("numberOfVisit", NumberOfVisit);
            }
            else
            {
                NumberOfVisit = 1;
            }
            UserTag = Preferences.Get("userTag", "");
            PageTitle = "Win Page";
            Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
            DrawLevels();
            WinText = UserTag + ", you have successfully landed on the island!";

        }
        private void OpenAppReviewPopup()
        {
            var dialog = new AppReviewPopupPage();
            dialog.BindingContext = this;
            PopupNavigation.Instance.PushAsync(dialog);
        }
        private void CheckNumberOfVisit()
        {
            if (NumberOfVisit == 1 || NumberOfVisit == 3 || NumberOfVisit == 10)
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
            await PopupNavigation.Instance.PopAsync();
        }

        public void CheckHighScore()
        {
            ShowText = "Your average is ";
            Device.BeginInvokeOnMainThread(() =>
            {
                var diffTimeListJson = Preferences.Get("listOfTimeAsJson", "");
                var DiffList = JsonConvert.DeserializeObject<List<double>>(diffTimeListJson);
                AverageTime = Math.Round(Math.Abs(DiffList.Sum() / DiffList.Count), 2);
                ShowAverageTime = AverageTime + " seconds";
                // PostScore();
            });
            if (Preferences.ContainsKey("playerScore"))
            {
                var prevousScore = Preferences.Get("playerScore", 0.0);
                if (AverageTime < prevousScore)
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

            Preferences.Set("levelNumber", 1);

        }
        public async Task PostScore()
        {
            try
            {
                //if (!IsViewBusy)
                //{
                IsViewBusy = true;
                //  await Task.Delay(2000);
                var addScoreService = new AddScoreService();
                var requestModel = new AddScoreRequestModel { Name = UserTag, Score = AverageTime.ToString(), Difficuilty = Preferences.Get("difficulty", Difficulty.Easy.ToString()) };
                var response = await addScoreService.AddScore(requestModel);
                if (response.Status != null)
                {
                    IsViewBusy = false;

                    if (response.Rank != 0)
                    {
                        ShowText = "You are now ranked ";
                        ShowAverageTime = "NO." + response.Rank;
                    }
                    CheckNumberOfVisit();
                    IsViewBusy = false;
                    // return false;
                    // });
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                // IsViewBusy = false;
                ShowText = "Play again to sharpen your brain";
                ShowAverageTime = "";
                Crashes.TrackError(ex);
            }
            //finally
            //{
            //    IsViewBusy = false;
            //}
        }
        private void TryAginCommandExcute(object obj)
        {
            Preferences.Set("levelNumber", 1);
            App.Current.MainPage.Navigation.PushAsync(new DificullityPage());
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

        }
    }
}
