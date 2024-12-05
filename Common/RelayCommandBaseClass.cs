using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaProject.Common
{
    internal class RelayCommandBaseClass : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommandBaseClass(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add =>
                // Ensure that the event handler is added correctly
                CommandManager.RequerySuggested += value;
            remove =>
                // Ensure that the event handler is removed correctly
                CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            // Ensure that CanExecute is efficient and doesn’t perform heavy operations
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            try
            {
                _execute(parameter);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($@"Command execution failed: {ex.Message}");
                // Depending on your needs, you might want to rethrow the exception or handle it differently
                throw;
            }
        }
    }
}

