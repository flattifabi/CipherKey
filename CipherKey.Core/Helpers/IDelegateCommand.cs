using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CipherKey.Core.Helpers
{
	public interface IDelegateCommand : ICommand
	{ }
	public class DelegateCommand : IDelegateCommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public DelegateCommand(Action execute) : this(execute, null)
		{
		}

		public DelegateCommand(Action execute, Func<bool> canExecute)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute?.Invoke() ?? true;
		}

		public void Execute(object parameter)
		{
			_execute();
		}
	}

	public class DelegateCommand<T> : IDelegateCommand
	{
		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;

		public DelegateCommand(Action<T> execute) : this(execute, null)
		{
		}

		public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		public bool CanExecute(object parameter)
		{
			if (parameter != null && parameter is T typedParameter)
			{
				return _canExecute?.Invoke(typedParameter) ?? true;
			}
			return _canExecute?.Invoke(default(T)) ?? true;
		}

		public void Execute(object parameter)
		{
			if (parameter != null && parameter is T typedParameter)
			{
				_execute(typedParameter);
			}
			else
			{
				_execute(default(T));
			}
		}
	}
}
