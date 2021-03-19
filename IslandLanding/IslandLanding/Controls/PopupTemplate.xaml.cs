using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IslandLanding.Controls
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class PopupTemplate 
  {
    public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(nameof(TitleText),
   typeof(string),
   typeof(PopupTemplate),
   string.Empty, BindingMode.TwoWay);
    public static readonly BindableProperty YesCommandProperty = BindableProperty.Create(nameof(YesCommand),
    typeof(ICommand),
    typeof(PopupTemplate),
    default(ICommand));
    public static readonly BindableProperty NoCommandProperty = BindableProperty.Create(nameof(NoCommand),
  typeof(ICommand),
  typeof(PopupTemplate),
  default(ICommand));
    public string TitleText
    {
      get => (string)GetValue(TitleTextProperty);
      set => SetValue(TitleTextProperty, value);
    }
    public ICommand YesCommand
    {
      get => (ICommand)GetValue(YesCommandProperty);
      set => SetValue(YesCommandProperty, value);
    }
    public ICommand NoCommand
    {
      get => (ICommand)GetValue(NoCommandProperty);
      set => SetValue(NoCommandProperty, value);
    }
    public PopupTemplate()
    {
      InitializeComponent();
    }
  }
}