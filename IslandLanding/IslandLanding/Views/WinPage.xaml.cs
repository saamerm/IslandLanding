using IslandLanding.ViewModel;
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
    public partial class WinPage : ContentPage
    {
        public WinViewModel winView;
        public WinPage()
        {
            InitializeComponent();
            winView = new WinViewModel();
            BindingContext = winView;
            if (Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopupStack.Any())
            {
                PopupNavigation.Instance.PopAsync();
            }

            Animation();
        }
        private void Animation()
        {
            winText.Opacity = 1;
            winParachut.Opacity = 1;
            dots.Opacity = 1;
            indicator.Opacity = 0;
            congratulationsText.Opacity = 0;
            WinTextStack.Opacity = 0;
            winButtonsStack.Opacity = 0;
            Device.StartTimer(new TimeSpan(0, 0, 3), () =>
            {
                Fading();
                return false;
            });
        }
        private async void Fading()
        {
            parachutAnimation.AutoPlay = false;
            var winFade = winText.FadeTo(0, 2000);
            var parachutFade = winParachut.FadeTo(0, 2000);
            var dotsFade = dots.FadeTo(0, 2000);
            // var viewIndicator = indicator.FadeTo(1, 2000);
            var congratulationsFade = congratulationsText.FadeTo(1, 2000);
            var WinTextStackFade = WinTextStack.FadeTo(1, 2000);
            var buttonFade = winButtonsStack.FadeTo(1, 2000);
            // indicator.IsRunning = true;
            await Task.WhenAll(winFade, parachutFade, dotsFade, congratulationsFade, WinTextStackFade, buttonFade, winView.PostScore());
        }
    }
}