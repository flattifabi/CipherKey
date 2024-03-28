using CipherKey.Core.Configurations;
using CipherKey.Core.Enums;
using CipherKey.Core.Events;
using CipherKey.Core.Extensions;
using CipherKey.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CipherKey.Core.Data;
using CipherKey.Crypt;

namespace CipherKey.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
	    private bool _isError = false;
	    private string _errorText = "Fehler";
		private SecureString _enteredLocalStoragePassword;
        private bool _isLoading;
		public event EventHandler<StorageConnectedEvent> LoginSuccess;
		public IDelegateCommand LoadLocalStorage => new DelegateCommand(EnteredLocalStorage);
		public IDelegateCommand LoadPathStorage => new DelegateCommand(EnteredPathStorage);

		public IDelegateCommand CloseCommand => new DelegateCommand(Close);

		private readonly Splash _view;
		private readonly IConfigurationService _configurationService;
        public SplashViewModel(IConfigurationService configurationService, Splash view)
        {
            _configurationService = configurationService;
			_view = view;
        }

        private void EnteredPathStorage()
        {
	        CipherF<CipherStorage>.Key = _view.RemotePassword.Password;
	        CipherF<CipherStorage>.Path = _view.RemotePassword.Text;
	        bool isValidPath = CipherF<CipherStorage>.IsRemoteSourceValid(_view.RemotePath.Text);
	        if (isValidPath)
	        {
		        var t = CipherF<CipherStorage>.Load();
		        if (t != null)
			        LoginSuccess?.Invoke(this, new StorageConnectedEvent() { StorageType = StorageType.Path, MasterPassword = _view.RemotePassword.Password});
		        else
					SimulateError(2, "Fehler bei der Anmeldung");
	        }else
		        SimulateError(2, "Der Pfad ist für CipherKey nicht gültig!");
        }
        
		private void EnteredLocalStorage()
		{
            var checkResult = _configurationService.CheckPassword(_view.localMasterPassword.Password).ResultData;
			if(!string.IsNullOrEmpty(checkResult))
				LoginSuccess?.Invoke(this, new StorageConnectedEvent { StorageType = StorageType.Local, MasterPassword = _view.localMasterPassword.Password });
			else
            {
				/* Error */
				SimulateError(2, "Falsches Passwort, überprüfe deine Eingaben!");
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

		private async void SimulateError(int delayInSeconds, string errorText = "")
		{
			ErrorText = errorText;
			IsError = true;
			await Task.Delay(delayInSeconds * 1000);
			IsError = false;
			OnPropertyChanged(nameof(IsError));
		}
		public bool IsError
		{
			get => _isError;
			set
			{
				_isError = value;
				OnPropertyChanged();
			}
		}

		public string ErrorText
		{
			get => _errorText;
			set
			{
				_errorText = value;
				OnPropertyChanged();
			}
		}
    }
}
