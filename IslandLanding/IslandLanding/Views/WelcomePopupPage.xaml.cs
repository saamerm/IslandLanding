using IslandLanding.ViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IslandLanding.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class WelcomePopupPage : PopupPage
  {
    public WelcomePopupPage()
    {
      InitializeComponent();
      BindingContext = new WelcomePopupViewModel();
    
    }
  }
}