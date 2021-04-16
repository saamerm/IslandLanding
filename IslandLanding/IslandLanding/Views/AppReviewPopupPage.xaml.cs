using IslandLanding.ViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace IslandLanding.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AppReviewPopupPage : PopupPage
  {
    public AppReviewPopupPage()
    {
      InitializeComponent();
      BindingContext = new EmptyPopupViewModel();
    }
  }
}