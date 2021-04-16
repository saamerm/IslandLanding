using IslandLanding.ViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace IslandLanding.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ThanksPopupPage : PopupPage
  {
    public ThanksPopupPage()
    {
      InitializeComponent();
      BindingContext = new EmptyPopupViewModel();
    }
  }
}