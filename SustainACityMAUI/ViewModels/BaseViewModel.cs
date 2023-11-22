using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Reflection;
using SustainACityMAUI.Commands;

namespace SustainACityMAUI.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary> Notifies UI of property changes. </summary>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        UpdateCommandStates();
    }

    private void UpdateCommandStates()
    {
        var commandProperties = GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => typeof(ICommand).IsAssignableFrom(p.PropertyType));

        foreach (var propertyInfo in commandProperties)
        {
            if (propertyInfo.GetValue(this) is BaseCommand command)
            {
                command.RaiseCanExecuteChanged();
            }
        }
    }
}