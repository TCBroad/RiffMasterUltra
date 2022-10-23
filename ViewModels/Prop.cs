namespace RiffMasterUltra.ViewModels;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;

public sealed class Prop<T> : INotifyPropertyChanged
{
    private T value;

    public T Value
    {
        get => this.value;
        set => this.SetField(ref this.value, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue)) return false;
        field = newValue;
        this.OnPropertyChanged(propertyName);
        return true;
    }
}

[ValueConversion(typeof(Prop<int>), typeof(int))]
public class PropConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((Prop<int>)value).Value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return new Prop<int> { Value = (int)value };
    }
}