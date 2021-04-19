using IslandLanding.Communication.Services;
using IslandLanding.Views;
using MediaManager;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class HomeViewModel : BaseViewModel
  {
    public ICommand ProfileCommand { get; set; }
    public ICommand LeaderBoardCommand { get; set; }
    public ICommand StartCommand { get; set; }
    public ICommand InfoCommand { get; set; }
    public ICommand PlayCommand { get; set; }
    public bool IsPlaying { get; set; }
    public string ButtonText { get; set; }
    public HomeViewModel()
    {
      ProfileCommand = new Command(ProfileCommandExcute);
      LeaderBoardCommand = new Command(LeaderBoardCommandExcute);
      StartCommand = new Command(StartCommandExcute);
      InfoCommand = new Command(InfoCommandExcute);
      PlayCommand = new Command(PlayCommandExcute);
      GetApiUrl();
      Notify();
      if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Any())
      {
        PopupNavigation.Instance.PopAsync();
      }
      PageTitle = "HomePage";
      MessagingCenter.Subscribe<ExitPopupViewModel, bool>(this, "isPlaying", (sender, _isPlaying) =>
         {
           IsPlaying = _isPlaying;
           ButtonText = (_isPlaying) ? "Music: On" : "Music: Off";
         });
      if (Preferences.ContainsKey("playMusic"))
      {
        IsPlaying = Preferences.Get("playMusic", false);
        ButtonText = (IsPlaying) ? "Music: On" : "Music: Off";
      }
      else
      {
        ButtonText ="Music: Off";
      }

      Analytics.TrackEvent(PageTitle);
    }

    private async void PlayCommandExcute(object obj)
    {
      if (!IsPlaying)
      {
        Preferences.Set("playMusic", true);
        IsPlaying = true;
        var audio = CrossMediaManager.Current;
        ButtonText = "Music: On";
        await audio.PlayFromAssembly("music.mp3", typeof(BaseViewModel).Assembly);
      }
      else
      {
        IsPlaying = false;
        ButtonText = "Music: Off";
        Preferences.Set("playMusic", false);
        await CrossMediaManager.Current.Stop();
      }
    }

    private void Notify()
    {
      if (!Preferences.Get("firstTime", false))
      {
        PopupNavigation.Instance.PushAsync(new WelcomePopupPage());
        Device.StartTimer(new TimeSpan(0, 0, 5), () =>
        {
          if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Any())
          {
            PopupNavigation.Instance.PopAsync();
          }
          return false;
        });
      }
    }
    private void InfoCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new InfoPage());
    }

    private void StartCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new DificullityPage());
    }

    private void LeaderBoardCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new LeaderBoardPage());
    }

    private void ProfileCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PushAsync(new SwitchUserAccountPage());
    }
    private async void GetApiUrl()
    {
      try
      {
        if (!IsBusy)
        {
          IsBusy = true;
          var getApiUrl = new GetGoogleSheetUrlService();
          var result = await getApiUrl.GetUrl();
          Helper.Constants.Api_Key = result;
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
