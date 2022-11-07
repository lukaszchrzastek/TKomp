using System;
using System.Windows.Input;

namespace TKomp.App.Commands
{
    public abstract class CommandBase : ICommand
    {
        private Action<object> _execte;
        private Predicate<object> _canExecute;        
        private event EventHandler CanExecuteChangedInternal;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public CommandBase(Action<object> execte, Predicate<object> canExecute) {
            _execte = execte;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null && _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execte(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {                
                handler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}