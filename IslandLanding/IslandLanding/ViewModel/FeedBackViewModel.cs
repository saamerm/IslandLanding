using IslandLanding.Communication.RequestModel;
using IslandLanding.Communication.Services.FeedBack;
using IslandLanding.Views;
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
    }
    private void BackCommandExcute(object obj)
    {
      App.Current.MainPage.Navigation.PopToRootAsync();
    }
    private async void SubmitCommandExcute(object obj)
    {
      try
      {
        if (!IsBusy)
        {
          IsBusy = true;
          var feedBackService = new FeedBackService();
          var result = await feedBackService.AddFeedback(AddFeedback);
          if (result.Status == "Success")
          {
            FeedbackAdded = result.Message;
            var dialog = new ThanksPopupPage();
            dialog.BindingContext = this;
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

      }
      catch (Exception e)
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
