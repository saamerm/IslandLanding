﻿using IslandLanding.ViewModel;
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
  public partial class DificullityPage : ContentPage
  {
    public DificullityPage()
    {
      InitializeComponent();
      BindingContext = new DifficuilityViewModel();
    }
  }
}