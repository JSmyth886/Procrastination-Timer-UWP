using System;
using System.Windows.Input;

namespace Procrastination_Timer.Common
{
  public class CommandHandler : ICommand
  {
    private readonly Action<object> action;
    private readonly bool canExecute;

    public CommandHandler(Action<object> action, bool canExecute)
    {
      this.action = action;
      this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
      return canExecute;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      action(parameter);
    }
  }
}
