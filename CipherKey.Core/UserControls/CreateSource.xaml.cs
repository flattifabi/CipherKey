using CipherKey.Core.Data;
using CipherKey.Core.Helpers;
using CipherKey.Crypt;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui;

namespace CipherKey.Core.UserControls
{
	/// <summary>
	/// Interaktionslogik für CreateSource.xaml
	/// </summary>
	public partial class CreateSource : UserControl, INotifyPropertyChanged
	{
		public EventHandler<string> RemoteAdressWantToAdd;
		private string _remoteCreateFolderPath;
		private string _remoteCreateMasterPassword;
		private string _remoteCreateFileName;
		private bool _remoteCreateRememberPasswordEnabled;

		private string _remotePath;
		public IDelegateCommand AddRemoteSourceCommand => new DelegateCommand(OnAddRemoteSource);
		public IDelegateCommand SelectRemoteCreateFolderPathCommand => new DelegateCommand(OnSelectRemoteCreateFolderPath);
		public IDelegateCommand SelectRemotePasswordSafeCommand => new DelegateCommand(OnSelectRemotePasswordSafe);

		public IDelegateCommand CreateRemoteSourceCommand => new DelegateCommand(OnCreateRemoteSource);

		private readonly ISnackbarService _snackbarService;
		public CreateSource(ISnackbarService snackbarService)
		{
			InitializeComponent();
			_snackbarService = snackbarService;
			DataContext = this;
		}
		private void OnAddRemoteSource()
		{
			RemoteAdressWantToAdd?.Invoke(this, RemotePath);
		}
		
		private void OnCreateRemoteSource()
		{
			var fullPath = System.IO.Path.Combine(RemoteCreateFolderPath, RemoteCreateFileName + ".cipher");

			CipherF<CipherStorage>.CreateIfNotExists(fullPath, RemoteCreateMasterPassword);
			var t = CipherF<CipherStorage>.LoadRemote(fullPath, RemoteCreateMasterPassword);
			t = new CipherStorage()
			{
				EnableToSavePassword = RemoteCreateRememberPasswordEnabled,
				IsRemote = true,
				ApplicationConfiguration = new Models.ApplicationConfiguration()
				{
					MasterPassword = RemoteCreateMasterPassword,
				}
			};

			var success = CipherF<CipherStorage>.SaveRemote(fullPath, RemoteCreateMasterPassword, t);
			if (success)
				_snackbarService.Show("Erstellt", $"Der geteilte Tresor {RemoteCreateFileName} wurde erfolgreich erstellt", Wpf.Ui.Controls.ControlAppearance.Success,null, new TimeSpan(0, 0, 5));
			else
				_snackbarService.Show("Fehler", $"Der geteilte Tresor {RemoteCreateFileName} konnte nicht erstellt werden", Wpf.Ui.Controls.ControlAppearance.Danger, null, new TimeSpan(0, 0, 5));
		}
		public string RemotePath
		{
			get => _remotePath;
			set
			{
				_remotePath = value;
				OnPropertyChanged();
			}
		}
		public string RemoteCreateFolderPath
		{
			get => _remoteCreateFolderPath;
			set
			{
				_remoteCreateFolderPath = value;
				OnPropertyChanged();
			}
		}
		public string RemoteCreateMasterPassword
		{
			get => _remoteCreateMasterPassword;
			set
			{
				_remoteCreateMasterPassword = value;
				OnPropertyChanged();
			}
		}
		public bool RemoteCreateRememberPasswordEnabled
		{
			get => _remoteCreateRememberPasswordEnabled;
			set
			{
				_remoteCreateRememberPasswordEnabled = value;
				OnPropertyChanged();
			}
		}
		public string RemoteCreateFileName
		{
			get => _remoteCreateFileName;
			set
			{
				_remoteCreateFileName = value;
				OnPropertyChanged();
			}
		}
		private void OnSelectRemoteCreateFolderPath()
		{

			var dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = true;

			if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
				RemoteCreateFolderPath = dialog.FileName;
		}
		private void OnSelectRemotePasswordSafe()
		{
			var dialog = new CommonOpenFileDialog();
			dialog.IsFolderPicker = false;
			dialog.Filters.Add(new CommonFileDialogFilter("CipherKey", "*.cipher"));
			if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
				RemotePath = dialog.FileName;
		}
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
