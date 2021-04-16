using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using IslandLanding.Helper;

namespace IslandLanding.ViewModel
{
  public class BaseViewModel : INotifyPropertyChanged
  {
    public bool IsBusy{get;set;}
    public string PageTitle { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;

    public BaseViewModel()
    {
        Analytics.TrackEvent("PageView: " + this.GetType().Name.Replace("ViewModel", string.Empty).SplitPascalCase());
    }

    protected void NotifyAllPropertiesChanged()
    {
      NotifyPropertyChanged(null);
    }

    protected bool SetProperty<T>(
        ref T backingStore,
        T value,
        [CallerMemberName] string propertyName = "")
    {
      if (EqualityComparer<T>.Default.Equals(backingStore, value))
      {
        return false;
      }

      backingStore = value;
      NotifyPropertyChanged(propertyName);

      return true;
    }

    protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
