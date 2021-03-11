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
  public partial class TutorialPage : ContentPage
  {
    public TutorialPage()
    {
      InitializeComponent();
      BindingContext = new TutorialViewModel();
    }
  }
}