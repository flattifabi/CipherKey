﻿using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Configurations;
using CipherKey.Core.Helpers;
using CipherKey.Core.Password;
using CipherKey.Core.UserControls;
using CipherKey.Services.Configuration;
using CipherKey.Services.Password;
using CipherKey.ViewModels;
using CipherKey.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
			_serviceProvider = services.BuildServiceProvider();
		}
		private void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<ISnackbarService, SnackbarService>();
			services.AddSingleton<IConfigurationService, ConfigurationService>();
			services.AddSingleton<IPasswordService, PasswordService>();

			services.AddSingleton<Splash, Splash>();
			services.AddSingleton<SplashViewModel, SplashViewModel>();

			services.AddSingleton<ConfigurationWindow, ConfigurationWindow>();
			services.AddSingleton<ConfigurationViewModel, ConfigurationViewModel>();

			services.AddSingleton<MainWindow, MainWindow>();
			services.AddSingleton<MainWindowViewModel, MainWindowViewModel>();

			services.AddSingleton<CreateTopic, CreateTopic>();
			services.AddSingleton<CreatePassword, CreatePassword>();
		}
		public void Run()
		{
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
				_mainWindowViewModel.MasterPassword = e.MasterPassword;
				_mainWindowViewModel.Initialize();
				_mainWindow.Show();
			};
		}
		public void InitializeDefaultComponents()
		{
			//var snackbarService = _serviceProvider.GetService<ISnackbarService>();
			//var mainWindow = _serviceProvider.GetService<MainWindow>();
			//snackbarService.SetSnackbarPresenter(mainWindow.SnackbarPresenter);
		}
	}
}
