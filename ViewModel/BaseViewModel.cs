using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace TestClient
{
    abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void InvokePropertyChanget(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T receivedValue, string PropertyName = null)
        {
            if (Equals(field, receivedValue)) return false;

            field = receivedValue;
            InvokePropertyChanget(PropertyName);
            return true;
        }
    }
}
