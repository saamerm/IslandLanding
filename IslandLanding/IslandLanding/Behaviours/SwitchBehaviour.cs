using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IslandLanding.Behaviours
{
 public class SwitchBehaviour : Behavior<Switch>
  {
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SwitchBehaviour), null);
    public static readonly BindableProperty EventNameProperty = BindableProperty.Create<SwitchBehaviour, string>(p => p.EventName, null);
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<SwitchBehaviour, object>(p => p.CommandParameter, null);
    public Switch Bindable { get; private set; }
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
    protected override void OnAttachedTo(Switch bindable)
    {
      base.OnAttachedTo(bindable);
      Bindable = bindable;
      Bindable.BindingContextChanged += OnBindingContextChanged;
      Bindable.Toggled += OnToggledChanged;
    }

    private void OnToggledChanged(object sender, ToggledEventArgs e)
    {
      Command?.Execute(sender);
    }

    protected override void OnDetachingFrom(Switch bindable)
    {
      base.OnDetachingFrom(bindable);
      Bindable.BindingContextChanged -= OnBindingContextChanged;
      Bindable.Toggled -= OnToggledChanged;
      Bindable = null;
    }
    private void OnBindingContextChanged(object sender, EventArgs e)
    {
      OnBindingContextChanged();
      BindingContext = Bindable.BindingContext;
    }
  }
}