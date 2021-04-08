using IslandLanding.ViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace IslandLanding.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class WinPopupPage : PopupPage
  {
    public WinPopupPage()
    {
      InitializeComponent();
      BindingContext = new EmptyPopupViewModel();
    }
  }
}