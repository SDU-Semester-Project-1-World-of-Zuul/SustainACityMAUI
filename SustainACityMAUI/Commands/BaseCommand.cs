using System.Windows.Input;

namespace SustainACityMAUI.Commands;

public abstract class BaseCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    public virtual bool CanExecute(object parameter)
    {
        return true; // By default, always able to execute
    }

    public abstract void Execute(object parameter);

    protected virtual void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RaiseCanExecuteChanged()
    {
        OnCanExecuteChanged();
    }
}