using System;
using System.Windows.Input;

namespace KTaskRemainder.Common
{
    public class CommandBase : ICommand
    {
        Action<object> _execute;
        Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged;

        public CommandBase(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
