using IslandLanding.Communication.RequestModel;
using IslandLanding.Communication.Services.FeedBack;
using IslandLanding.Views;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IslandLanding.ViewModel
{
  public class FeedBackViewModel:BaseViewModel
  {
    public ICommand SubmitCommand { get; set; }
    public ICommand BackCommand { get; set; }
    public string FeedbackAdded { get; set; }
    public  AddFeedbackRequestModel AddFeedback { get; set; }

    public FeedBackViewModel()
    {
      AddFeedback = new AddFeedbackRequestModel();
      BackCommand = new Command(BackCommandExcute);
      SubmitCommand = new Command(SubmitCommandExcute);
      if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Any())
      {
        PopupNavigation.Instance.PopAsync();
      }
      PageTitle = "FeedBack Page";
      Analytics.TrackEvent("Page", new Dictionary<string, string> { { "Value", PageTitle } });
    }
    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopAsync();
    }
    private async void SubmitCommandExcute(object obj)
    {
      try
      {
        if (!IsBusy)
        {
          IsBusy = true;
          var dialog = new ThanksPopupPage();
          dialog.BindingContext = this;
          if (!string.IsNullOrEmpty(AddFeedback.Name)&& !string.IsNullOrEmpty(AddFeedback.Email)&& !string.IsNullOrEmpty(AddFeedback.Feedback))
          {
            var feedBackService = new FeedBackService();
            var result = await feedBackService.AddFeedback(AddFeedback);
           
            if (result.Status == "Success")
            {
              FeedbackAdded = "Thank you for your feedback";
              await PopupNavigation.Instance.PushAsync(dialog);
              Device.StartTimer(new TimeSpan(0, 0, 5), () =>
              {
                App.Current.MainPage.Navigation.PushAsync(new HomePage());
                return false;
              });
            }
            else
            {
              FeedbackAdded = "Something wrong happen try agian later";
              await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
          }
          else
          {
            FeedbackAdded = "Please fill out all the fields";
            await PopupNavigation.Instance.PushAsync(dialog);
          }
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
