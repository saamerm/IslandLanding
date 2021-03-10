using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace IslandLanding.ViewModel
{
  public class BaseViewModel : INotifyPropertyChanged
  {

    public event PropertyChangedEventHandler PropertyChanged;
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
