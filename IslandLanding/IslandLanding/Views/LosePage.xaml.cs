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
    }
  }
}