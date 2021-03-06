using IslandLanding.Models;
using IslandLanding.ViewModel;
using Rg.Plugins.Popup.Pages;
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
  public partial class NextPopupPage : PopupPage
  {
    public NextPopupPage(GameModel gameModel)
    {
      InitializeComponent();
      BindingContext = new NextPopupViewModel(gameModel);
    }
  }
}