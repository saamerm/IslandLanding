using IslandLanding.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IslandLanding
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();
      Sharpnado.Shades.Initializer.Initialize(false);
      Sharpnado.Tabs.Initializer.Initialize(false, false);
      if (string.IsNullOrEmpty(Preferences.Get("userTag", "")))
      {
        MainPage = new NavigationPage(new GamerTagPage());
      }
      else
      {
        MainPage = new NavigationPage(new HomePage());
      }
     
    }

    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
  }
}
