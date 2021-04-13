using IslandLanding.ViewModel;
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
  public partial class WinPage : ContentPage
  {
    public WinPage()
    {
      InitializeComponent();
      BindingContext = new WinViewModel();
    }
    private async void WinAnimation()
    {
      winText.Opacity = 1;
      winParachut.Opacity = 1;
      dots.Opacity = 1;
      congratulationsText.Opacity = 0;
      WinTextStack.Opacity = 0;
      winButtonsStack.Opacity = 0;
      await winText.FadeTo(0, 4000);
      await winParachut.FadeTo(0, 4000);
      await congratulationsText.FadeTo(1, 4000);
      await winText.FadeTo(1, 4000);
      await winButtonsStack.FadeTo(1, 2000);


    }
  }
}