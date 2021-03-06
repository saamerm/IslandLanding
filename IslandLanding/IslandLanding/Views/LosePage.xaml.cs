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
    private  void Animation()
    {
      loseText.Opacity = 1;
     // parachut.Opacity = 1;
      loseGameText.Opacity = 0;
      lake.Opacity = 0;
      buttonsStack.Opacity = 0;
      textStack.Opacity = 0;
      Device.StartTimer(new TimeSpan(0, 0, 3), () =>
         {
           Fading();
           return false;
         });
    }
    private async void Fading()
    {
      var loseTextFade= loseText.FadeTo(0, 2000);
     // var x1= parachut.FadeTo(0, 2000);
      var loseGameTextFade= loseGameText.FadeTo(1, 2000);
      var textStackFade= textStack.FadeTo(1, 2000);
      var buttonsStackFade= buttonsStack.FadeTo(1, 2000);
      var lakeFade= lake.FadeTo(1, 2000);
      await Task.WhenAll(loseTextFade,loseGameTextFade,textStackFade,buttonsStackFade,lakeFade);
    }
  }
}