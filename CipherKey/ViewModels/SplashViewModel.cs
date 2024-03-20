using CipherKey.Core.Configurations;
using CipherKey.Core.Enums;
using CipherKey.Core.Events;
using CipherKey.Core.Extensions;
using CipherKey.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CipherKey.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
		private SecureString _enteredLocalStoragePassword;
        private bool _isLoading;
		public event EventHandler<StorageConnectedEvent> LoginSuccess;
		public IDelegateCommand LoadLocalStorage => new DelegateCommand(EnteredLocalStorage);
		public IDelegateCommand LoadPathStorage { get; set; }
        public IDelegateCommand LoadRemoteStorage { get; set; }
		public IDelegateCommand CloseCommand => new DelegateCommand(Close);

		private readonly Splash _view;
		private readonly IConfigurationService _configurationService;
        public SplashViewModel(IConfigurationService configurationService, Splash view)
        {
            _configurationService = configurationService;
			_view = view;
        }

		private void EnteredLocalStorage()
		{
            var checkResult = _configurationService.CheckPassword(_view.localMasterPassword.Password).ResultData;
			if(!string.IsNullOrEmpty(checkResult))
				LoginSuccess?.Invoke(this, new StorageConnectedEvent { StorageType = StorageType.Local, MasterPassword = _view.localMasterPassword.Password });
			else
            {
				/* Error */
			}
		}


		private void Close()
		{
			Application.Current.Shutdown();
		}
		public bool IsLoading
		{
			get => _isLoading;
			set
			{
				_isLoading = value;
				OnPropertyChanged();
			}
		}
        public SecureString EnteredLocalStoragePassword
		{
			get => _enteredLocalStoragePassword;
			set
			{
				_enteredLocalStoragePassword = value;
				OnPropertyChanged();
			}
		}
    }
}
