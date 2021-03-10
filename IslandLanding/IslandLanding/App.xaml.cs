using IslandLanding.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IslandLanding
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      MainPage = new GamerTagPage();
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
