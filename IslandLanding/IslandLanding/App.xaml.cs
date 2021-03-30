using IslandLanding.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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
      AppCenter.Start("ios=3bf694c6-7e7d-49a6-a321-af2e35cdeb28;" +
                  "android=ab9336e4-5a47-498c-81e7-99885f2ec59e;",
                  typeof(Analytics), typeof(Crashes));
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
  }
}
