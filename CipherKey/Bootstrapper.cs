using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Configurations;
using CipherKey.Core.Data;
using CipherKey.Core.Helpers;
using CipherKey.Core.Logging;
using CipherKey.Core.Password;
using CipherKey.Core.SafeConnect;
using CipherKey.Core.UserControls;
using CipherKey.Crypt;
using CipherKey.Services.Configuration;
using CipherKey.Services.Logging;
using CipherKey.Services.Password;
using CipherKey.Services.SafeConnection;
using CipherKey.ViewModels;
using CipherKey.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAPICodePack.Dialogs;
using Module.Passwords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;

namespace CipherKey
{
    public class Bootstrapper
	{
		private Splash _startupView;
		private SplashViewModel _startupViewModel;
		private MainWindow _mainWindow;
		private MainWindowViewModel _mainWindowViewModel;
		private ConfigurationWindow _configurationView;
		private ConfigurationViewModel _configurationViewModel;
		private readonly IServiceProvider _serviceProvider;
		public Bootstrapper()
		{
			var services = new ServiceCollection();
			ConfigureServices(services);
			ConfigureCipherFile();
			_serviceProvider = services.BuildServiceProvider();
		}
		private void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<ILogService, LogService>();
			services.AddSingleton<ISnackbarService, SnackbarService>();
			services.AddSingleton<IContentDialogService, ContentDialogService>();
			services.AddSingleton<IConfigurationService, ConfigurationService>();
			services.AddSingleton<IPasswordService, PasswordService>();
			services.AddSingleton<ISafeConnectService, SafeConnectService>();

			services.AddSingleton<Splash, Splash>();
			services.AddSingleton<SplashViewModel, SplashViewModel>();

			services.AddSingleton<ConfigurationWindow, ConfigurationWindow>();
			services.AddSingleton<ConfigurationViewModel, ConfigurationViewModel>();

			services.AddSingleton<MainWindow, MainWindow>();
			services.AddSingleton<MainWindowViewModel, MainWindowViewModel>();

			services.AddSingleton<CreateTopic, CreateTopic>();
			services.AddSingleton<CreatePassword, CreatePassword>();
			services.AddSingleton<EditPassword, EditPassword>();
			services.AddSingleton<PasswordBackupList, PasswordBackupList>();
			services.AddSingleton<CreateSource, CreateSource>();
			services.AddSingleton<CreateTopicRemote, CreateTopicRemote>();

			/* Modules */
			services.AddSingleton<PasswordModuleView>();
			services.AddSingleton<PasswordModuleViewModel>();
			services.AddSingleton<PasswordModule>();
		}
		public void Run()
		{
			CipherF<CipherStorage>.Path = FilePaths.CipherStorageFilePath;
			CipherF<CipherStorage>.Key = IDGenerator.GetComputerID();

			_serviceProvider.GetService<IConfigurationService>().Initialize();
			FolderCryptor.DecryptFolder(FilePaths.PasswordStorageFilePath, IDGenerator.GetComputerID());
			var configurationService = _serviceProvider.GetService<IConfigurationService>();
			_startupView = _serviceProvider.GetService<Splash>() ?? new();
			_startupViewModel = _serviceProvider.GetService<SplashViewModel>();
			
			_mainWindow = _serviceProvider.GetService<MainWindow>();
			_mainWindowViewModel = _serviceProvider.GetService<MainWindowViewModel>();

			_configurationView = _serviceProvider.GetService<ConfigurationWindow>();
			_configurationViewModel = _serviceProvider.GetService<ConfigurationViewModel>();

			_mainWindow.DataContext = _mainWindowViewModel;
			_startupView.DataContext = _startupViewModel;
			_configurationView.DataContext = _configurationViewModel;

			DoEventSubscription();
			InitializeDefaultComponents();

			if (!configurationService.IsConfigured().ResultData)
				_configurationView.Show();
			else
				_startupView.Show();
		}
		public void DoEventSubscription()
		{
			_configurationViewModel.ConfigurationFinished += () =>
			{
				_configurationView.Close();
				_startupView.Show();
			};
			_startupViewModel.LoginSuccess += (sender, e) =>
			{
				_startupView.Close();
				//_mainWindowViewModel.MasterPassword = e.MasterPassword;
				_serviceProvider.GetService<PasswordModuleViewModel>().MasterPassword = e.MasterPassword;
				_mainWindowViewModel.Initialize();
				_mainWindow.Show();
			};
		}
		public void InitializeDefaultComponents()
		{
			var snackbarService = _serviceProvider.GetService<ISnackbarService>();
			var dialogService = _serviceProvider.GetService<IContentDialogService>();
			var mainWindow = _serviceProvider.GetService<MainWindow>();

			dialogService.SetContentPresenter(mainWindow.ContentPresenter);
			snackbarService.SetSnackbarPresenter(mainWindow.SnackbarPresenter);
		}
		public void ConfigureCipherFile()
		{
			FilePaths.CreateBaseFilePath();
			CipherF<CipherStorage>.Path = FilePaths.CipherStorageFilePath;
			CipherF<CipherStorage>.Key = IDGenerator.GetComputerID();
			CipherF<CipherStorage>.CreateIfNotExist();
		}
	}
}
