using CipherKey.Core.Configurations;
using CipherKey.Core.Helpers;
using CipherKey.Core.Interfaces;
using CipherKey.Core.Password;
using CipherKey.Core.UserControls;
using Module.Passwords;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CipherKey.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private IModuleBase _selectedModule;
		internal string MasterPassword;

		public ObservableCollection<IModuleBase> Modules { get; set; } = new ObservableCollection<IModuleBase>();

        public IDelegateCommand CloseCommand => new DelegateCommand(() => Application.Current.Shutdown());
        public IDelegateCommand MinimizeCommand => new DelegateCommand(() =>
        {
            _mainWindow.WindowState = WindowState.Minimized;
        });

        private readonly MainWindow _mainWindow;
        private readonly PasswordModule _passwordModule;
        public MainWindowViewModel(PasswordModule passwordModule, MainWindow mainWindow)
        {
            _passwordModule = passwordModule;
            _mainWindow = mainWindow;
        }
        public void Initialize()
        {
            Modules.Add(_passwordModule);
            foreach(var module in Modules)
                module.Initialize();

            SelectedModule = Modules.First();
        }
        public IModuleBase SelectedModule
        {
			get => _selectedModule;
			set
            {
				_selectedModule = value;
				OnPropertyChanged();
			}
		}
	}
}
