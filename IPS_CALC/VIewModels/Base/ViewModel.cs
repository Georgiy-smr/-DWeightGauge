using IPS_CALC.EnumsAndDictinary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Threading;

namespace IPS_CALC.VIewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected Dispatcher _Dispatcher;
        protected virtual void OnPropertyChanged( [CallerMemberName] string PropertyName = null)
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;

            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
