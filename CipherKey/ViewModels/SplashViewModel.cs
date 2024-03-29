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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CipherKey.Core.Data;
using CipherKey.Core.UserControls;
using CipherKey.Crypt;
using Microsoft.WindowsAPICodePack.Dialogs;
using Application = System.Windows.Application;

namespace CipherKey.ViewModels
{
    public class SplashViewModel : BaseViewModel
    {
	    private bool _isError = false;
	    private string _errorText = "Fehler";
	    private Control _moduleView;
		private SecureString _enteredLocalStoragePassword;
		private string _enteredRemotePath;
        private bool _isLoading;
		public event EventHandler<StorageConnectedEvent> LoginSuccess;
		public IDelegateCommand LoadLocalStorage => new DelegateCommand(EnteredLocalStorage);
		public IDelegateCommand LoadPathStorage => new DelegateCommand(EnteredPathStorage);
		public IDelegateCommand CloseCommand => new DelegateCommand(Close);
		public IDelegateCommand CreateSourceCommand => new DelegateCommand(CreateSource);

		public IDelegateCommand SelectedRemoteSourceFromFileSystemCommand =>
			new DelegateCommand(SelectedRemoteSourceFromFileSystem);

		

		private readonly Splash _view;
		private readonly CreateSource _createSourceView;
		private readonly IConfigurationService _configurationService;
        public SplashViewModel(IConfigurationService configurationService, Splash view, CreateSource createSourceView)
        {
            _configurationService = configurationService;
			_view = view;
			_createSourceView = createSourceView;
			
			DoEventSubscription();
			SetDefaultValues();
        }

        private void SetDefaultValues()
        {
	        var applicationConfiguration = _configurationService.GetApplicationConfiguration().ResultData;
	        EnteredRemotePath = applicationConfiguration?.LastConnectedFilePath ?? string.Empty;
        }
        private void DoEventSubscription()
        {
	        _view.Window.MouseDown += OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
	        CloseModuleView();
        }

        private void EnteredPathStorage()
        {
	        CipherF<CipherStorage>.Key = _view.RemotePassword.Password.Hash();
	        CipherF<CipherStorage>.Path = EnteredRemotePath;
	        bool isValidPath = CipherF<CipherStorage>.IsRemoteSourceValid(_view.RemotePath.Text);
	        if (isValidPath)
	        {
		        var t = CipherF<CipherStorage>.Load();
		        if (t != null)
		        {
			        LoginSuccess?.Invoke(this, new StorageConnectedEvent() { StorageType = StorageType.Path, MasterPassword = _view.RemotePassword.Password});
			        var currentApplicationConfiguration =
				        _configurationService.GetApplicationConfiguration().ResultData;
			        if (currentApplicationConfiguration != null)
			        {
				        currentApplicationConfiguration.LastConnectedFilePath = EnteredRemotePath;
				        _configurationService.UpdateApplicationConfiguration(currentApplicationConfiguration);
			        }
		        }
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
		private void CreateSource()
		{
			SetModuleView(_createSourceView);
		}
		private void SelectedRemoteSourceFromFileSystem()
		{
			var dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = false;
			dialog.Filters.Add(new CommonFileDialogFilter("CipherKey", "*.cipher"));
			if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
				EnteredRemotePath = dialog.FileName;
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
		private void CloseModuleView()
		{
			var storyboard = _view.FindResource("CollapseModuleView") as Storyboard;
			storyboard.Begin();
		}
		private void SetModuleView(Control control)
		{
			var storyboard = _view.FindResource("ExpandModuleView") as Storyboard;
			storyboard.Begin();
			ModuleView = control;
			OnPropertyChanged(nameof(ModuleView));
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

		public Control ModuleView
		{
			get => _moduleView;
			set
			{
				_moduleView = value;
				OnPropertyChanged();
			}
		}

		public string EnteredRemotePath
		{
			get => _enteredRemotePath;
			set
			{
				_enteredRemotePath = value;
				OnPropertyChanged();
			}
		}
    }
}
