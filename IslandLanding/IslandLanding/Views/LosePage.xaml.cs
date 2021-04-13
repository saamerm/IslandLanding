using IslandLanding.Models;
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
  public partial class LosePage : ContentPage
  {
    public LosePage(GameModel game)
    {
      InitializeComponent();
      BindingContext = new LoseViewModel(game);
      Animation();

    }
    private async void Animation()
    {
      loseText.Opacity = 1;
      parachut.Opacity = 1;
      lake.Opacity = 0;
      buttonsStack.Opacity = 0;
      textStack.Opacity = 0;
      await loseText.FadeTo(0, 4000);
      await parachut.FadeTo(0, 4000);
      await textStack.FadeTo(1, 4000);
      await buttonsStack.FadeTo(1, 4000);
      await lake.FadeTo(1, 2000);
    
     
    }
  }
}