using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IslandLanding.Behaviours
{
 public class ButtonBehviour : Behavior<Button>
  {
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonBehviour), null);
    public static readonly BindableProperty EventNameProperty = BindableProperty.Create<ButtonBehviour, string>(p => p.EventName, null);
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<ButtonBehviour, object>(p => p.CommandParameter, null);
    public static readonly BindableProperty ReleasedProperty = BindableProperty.Create(nameof(Released), typeof(ICommand), typeof(ButtonBehviour), null);
    public Button Bindable { get; private set; }
    public string EventName
    {
      get { return (string)GetValue(EventNameProperty); }
      set { SetValue(EventNameProperty, value); }
    }
    public object CommandParameter
    {
      get { return GetValue(CommandParameterProperty); }
      set { SetValue(CommandParameterProperty, value); }
    }
    public ICommand Command
    {
      get { return (ICommand)GetValue(CommandProperty); }
      set { SetValue(CommandProperty, value); }
    }
    public ICommand Released
    {
      get { return (ICommand)GetValue(ReleasedProperty); }
      set { SetValue(ReleasedProperty, value); }
    }
    protected override void OnAttachedTo(Button bindable)
    {
      base.OnAttachedTo(bindable);
      Bindable = bindable;
      Bindable.BindingContextChanged += OnBindingContextChanged;
      Bindable.Pressed += OnPressed;
      Bindable.Released += Bindable_Released;
    }

    private void Bindable_Released(object sender, EventArgs e)
    {
      Released?.Execute(sender);
    }

    private void OnPressed(object sender, EventArgs e)
    {
      Command?.Execute(sender);
    }

    protected override void OnDetachingFrom(Button bindable)
    {
      base.OnDetachingFrom(bindable);
      Bindable.BindingContextChanged -= OnBindingContextChanged;
      Bindable.Pressed -= OnPressed;
      Bindable.Released -= Bindable_Released;
      Bindable = null;
    }
    private void OnBindingContextChanged(object sender, EventArgs e)
    {
      OnBindingContextChanged();
      BindingContext = Bindable.BindingContext;
    }
  }
}