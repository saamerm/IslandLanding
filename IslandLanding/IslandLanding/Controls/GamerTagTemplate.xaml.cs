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
  public partial class GamerTagTemplate 
  {
    public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(nameof(TitleText),
    typeof(string),
    typeof(GamerTagTemplate),
    default(string));
    public static readonly BindableProperty ButtonTextProperty = BindableProperty.Create(nameof(ButtonText),
    typeof(string),
    typeof(GamerTagTemplate),
    default(string));
    public static readonly BindableProperty GamerTagProperty = BindableProperty.Create(nameof(GamerTag),
    typeof(string),
    typeof(GamerTagTemplate),
    string.Empty, BindingMode.TwoWay);
    public static readonly BindableProperty SubmitCommandProperty = BindableProperty.Create(nameof(SubmitCommand),
    typeof(ICommand),
    typeof(GamerTagTemplate),
    default(ICommand));
    public ICommand SubmitCommand
    {
      get => (ICommand)GetValue(SubmitCommandProperty);
      set => SetValue(SubmitCommandProperty, value);
    }
    public string TitleText
    {
      get => (string)GetValue(TitleTextProperty);
      set => SetValue(TitleTextProperty, value);
    }
    public string ButtonText
    {
      get => (string)GetValue(ButtonTextProperty);
      set => SetValue(ButtonTextProperty, value);
    }
    public string GamerTag
    {
      get => (string)GetValue(GamerTagProperty);
      set => SetValue(GamerTagProperty, value);
    }
    public GamerTagTemplate()
    {
      InitializeComponent();
    }
  }
}