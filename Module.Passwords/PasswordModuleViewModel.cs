using CipherKey.Core.Configurations;
using CipherKey.Core.Helpers;
using CipherKey.Core.Password;
using CipherKey.Core.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;
using Wpf.Ui;
using CipherKey.Crypt;
using CipherKey.Core.Data;
using Wpf.Ui.Extensions;
using Wpf.Ui.Controls;
using CipherKey.Core.Dialogs;
using CipherKey.Core.Extensions;
using CipherKey.Core.SafeConnect;

namespace Module.Passwords
{
	public class PasswordModuleViewModel : BaseViewModel
	{
		#region Fields

		private readonly IConfigurationService _configurationService;
		private readonly IPasswordService _passwordService;
		private readonly ISnackbarService _snackbarService;
		private readonly IContentDialogService _contentDialogService;
		private readonly ISafeConnectService _safeConnectService;

		private readonly CreateTopicRemote _createTopicRemote;
		private readonly CreatePassword _createPasswordView;
		private readonly CreateTopic _createTopicView;
		private readonly EditPassword _editPassword;
		private readonly PasswordBackupList _backupList;
		private readonly CreateSource _createSource;
		private readonly PasswordModuleView _view;
		private Control _moduleView;
		private Topic _selectedTopic;

		#endregion Fields


		#region Public Constructors

		public PasswordModuleViewModel(IConfigurationService configurationService, IPasswordService passwordService, ISnackbarService snackbarService,
			PasswordModuleView view, CreateTopic createTopicView, CreatePassword createPasswordView, EditPassword editPassword, 
			PasswordBackupList backupList, CreateSource createSource, IContentDialogService contentDialogService, ISafeConnectService safeConnectService,
			CreateTopicRemote createTopicRemote)
		{
			_configurationService = configurationService;
			_view = view;
			_createTopicView = createTopicView;
			_passwordService = passwordService;
			_createPasswordView = createPasswordView;
			_snackbarService = snackbarService;
			_editPassword = editPassword;
			_backupList = backupList;
			_createSource = createSource;
			_contentDialogService = contentDialogService;
			_safeConnectService = safeConnectService;
			_createTopicRemote = createTopicRemote;
		}

		#endregion Public Constructors


		#region Properties

		public IDelegateCommand CloseCommand => new DelegateCommand(Close);
		public IDelegateCommand CopyPasswordCommand => new DelegateCommand<PasswordBase>(OnCopyPassword);
		public IDelegateCommand CopyUsernameCommand => new DelegateCommand<PasswordBase>(OnCopyUsername);
		public IDelegateCommand CreatePasswordCommand => new DelegateCommand(OpenCreatePassword);
		public IDelegateCommand CreateTopicCommand => new DelegateCommand(OpenCreateTopic);
		public IDelegateCommand DeletePasswordEntry => new DelegateCommand<PasswordBase>(OnDeletePasswordEntry);
		public IDelegateCommand EditPasswordCommand => new DelegateCommand<PasswordBase>(OnEditPasswordEntry);
		public IDelegateCommand OpenPasswordBackupCommand => new DelegateCommand<PasswordBase>(OnOpenPasswordBackup);
		public IDelegateCommand AddSourceCommand => new DelegateCommand(OnAddSource);
		public IDelegateCommand RemoveRemoteConnectionCommand => new DelegateCommand<RemoteAdressData>(OnRemoveRemoteConnection);
		public IDelegateCommand AddTopicToRemoteConnectionCommand => new DelegateCommand<RemoteAdressData>(OnAddTopicToRemoteConnection);

		public string MasterPassword { get; set; } = string.Empty;
		public Control ModuleView
		{
			get => _moduleView;
			set
			{
				_moduleView = value;
				OnPropertyChanged(nameof(ModuleView));
			}
		}

		public ObservableCollection<PasswordBase> Passwords { get; set; }
		public ObservableCollection<RemoteAdressData> Addresses { get; set; }
		public Topic SelectedTopic
		{
			get => _selectedTopic;
			set
			{
				_selectedTopic = value;
				LoadPasswordsForSelectedTopic();
				OnPropertyChanged(nameof(SelectedTopic));
			}
		}

		public ObservableCollection<Topic> Topics { get; set; }

		#endregion Properties


		#region Public Methods

		public async void Initialize()
		{
			Topics = new ObservableCollection<Topic>(_configurationService.GetTopics().ResultData);
			Addresses = new ObservableCollection<RemoteAdressData>(_configurationService.GetRemoteAdresses().ResultData);

            _createTopicView.TopicCreated += OnTopicCreated;
			_createPasswordView.PasswordCreated += OnPasswordCreated;
			_editPassword.PasswordChanged += OnPasswordChanged;
			_createSource.RemoteAdressWantToAdd += OnSourceWantToAdd;
			_backupList.PasswordBackupSetEvent += (sender, e) =>
			{
				LoadPasswordsForSelectedTopic();
				_passwordService.RestorePasswordFromBackup(e.PasswordBase, e.PasswordBackupData);
				_snackbarService.Show("Wiederhergestellt", "Das Passwort wurde wiederhergestellt", Wpf.Ui.Controls.ControlAppearance.Success, null, new TimeSpan(0, 0, 5));
				CloseModuleView();
			};

			if(Addresses.Where(x => !string.IsNullOrEmpty(x.Password)).Count() != 0)
			{
				var result = await _safeConnectService.ConnectAllAvailabelSafes(Addresses.Where(x => !string.IsNullOrEmpty(x.Password)).ToList(), MasterPassword);
				if (result) _snackbarService.Show("Erfolgreich", "Alle Tresore wurden erfolgreich verbunden", Wpf.Ui.Controls.ControlAppearance.Success, null, new TimeSpan(0, 0, 5));
				else _snackbarService.Show("Fehler", "Es konnten nicht alle Tresore verbunden werden", Wpf.Ui.Controls.ControlAppearance.Caution, null, new TimeSpan(0, 0, 5));
			}
			SelectedTopic = Topics.FirstOrDefault();
			foreach(var address in Addresses)
				address.Changed();
			OnPropertyChanged(nameof(Topics));
		}
		private async void OnAddTopicToRemoteConnection(RemoteAdressData data)
		{
			AddRemoteTopicDialog addRemoteTopicDialog = new();
			var result = await _contentDialogService.ShowAsync(new ContentDialog()
			{
				Content = addRemoteTopicDialog,
				PrimaryButtonText = "OK",
			}, new CancellationToken());
			if(result.ToString() != "Primary") return;
			if(result.ToString() == "Primary")
			{

				if(string.IsNullOrEmpty(addRemoteTopicDialog.Topic.Name))
				{
					_snackbarService.Show("Fehler", "Der Name des Themas darf nicht leer sein", Wpf.Ui.Controls.ControlAppearance.Caution, null, new TimeSpan(0, 0, 5));
					return;
				}
				var topic = new Topic()
				{
					Name = addRemoteTopicDialog.Topic.Name,
					Description = addRemoteTopicDialog.Topic.Description,
					Design = addRemoteTopicDialog.Topic.Design
				};
				_configurationService.AddTopic(topic, new SourceConfiguration()
				{
					Password = data.Password,
					Path = data.FilePath,
					LocalMasterPassword = MasterPassword
				});
				//Topics.Add(topic);
				data.CipherStorage.Topics.Add(topic);
				data.Changed();
				_snackbarService.Show("Erfolgreich", "Die Kategorie wurde erfolgreich hinzugefügt", Wpf.Ui.Controls.ControlAppearance.Success, null, new TimeSpan(0, 0, 5));
			}

			//_createTopicRemote.SetSourceContext(new SourceConfiguration()
			//{
			//	Path = data.FilePath,
			//	Password = data.Password,
			//});
		}
		private async void OnSourceWantToAdd(object? sender, string e)
		{
			var checkIfSourceIsValid = CipherF<CipherStorage>.IsRemoteSourceValid(e);
			if (!checkIfSourceIsValid)
			{
				_snackbarService.Show("Fehler", "Der Tresorpfad ist nicht gültig. Überprüfe den Pfad und die Datei", Wpf.Ui.Controls.ControlAppearance.Caution, null, new TimeSpan(0, 0, 5));
				return;
			}

			var inputDialog = new InputDialog();
			inputDialog.Title = "Passwort eingeben";
			inputDialog.Text = "Gib das Master-Passwort für den geteilten Tresor ein";
			var result = await _contentDialogService.ShowAsync(new ContentDialog()
			{
				Content = inputDialog,
				PrimaryButtonText = "OK",
			}, new CancellationToken());

			if(result.ToString() != "Primary") return;
			if(result.ToString() == "Primary")
			{
				if (!string.IsNullOrEmpty(inputDialog.GetPassword()))
				{
					var t = CipherF<CipherStorage>.Load(e, inputDialog.GetPassword().Hash());
					if (t != null)
					{
						var remoteAddressData = new RemoteAdressData()
						{
							FilePath = e,
							PersonalName = e,
							RemoteAddressState = CipherKey.Core.Enums.RemoteAddressState.Connected,
							Password = inputDialog.GetPassword(),
						};
						if (t.EnableToSavePassword)
							remoteAddressData.Password = _passwordService.GetEncryptedPassword(inputDialog.GetPassword(), MasterPassword).ResultData;

						var textInputDialog = new TextInputDialog();
						textInputDialog.Title = "Anzeige Name";
						textInputDialog.Text = "Gib einen Namen an wie der Tresor bei dir angezeigt werden soll. Diesen Namen kannst nur du sehen";
						var textInputDialogResult = await _contentDialogService.ShowAsync(new ContentDialog()
						{
							Content = textInputDialog,
							PrimaryButtonText = "OK",
						}, new CancellationToken());
						if(textInputDialogResult.ToString() == "Primary")
						{
							remoteAddressData.PersonalName = textInputDialog.Input;
						}
                        Addresses.Add(remoteAddressData);
						_configurationService.AddRemoteAddress(remoteAddressData);
					}else
					{
						_snackbarService.Show("Fehler", "Das Master-Passwort ist nicht gültig wodurch diese Datei nicht entschlüsselt werden kann", ControlAppearance.Danger, null, new TimeSpan(0, 0, 5));
					}
				}
				else
					_snackbarService.Show("Eingabe ungültig", "Deine Eingabe war unvollständig. Das Passwortfeld darf nicht leer sein", ControlAppearance.Caution, null, new TimeSpan(0, 0, 5));
			}
			Console.WriteLine("Test");
		}

		#endregion Public Methods


		#region Private Methods

		private void Close()
		{
			Application.Current.Shutdown();
		}

		private void CloseModuleView()
		{
			var storyboard = _view.FindResource("CollapseModuleView") as Storyboard;
			storyboard.Begin();
		}

		private void LoadPasswordsForSelectedTopic()
		{
			if (SelectedTopic == null)
			{
				return;
			}
			Passwords = new ObservableCollection<PasswordBase>(_passwordService.GetPasswordsByTopic(SelectedTopic.Name).ResultData);
			OnPropertyChanged(nameof(Passwords));
		}

		private void OnCopyPassword(PasswordBase @base)
		{
			_snackbarService.Show("Kopiert", "Das Passwort wurde in die Zwischenablage kopiert", Wpf.Ui.Controls.ControlAppearance.Success, null, new TimeSpan(0, 0, 5));
			var currentText = Clipboard.GetText();
			var encryptedPassword = _passwordService.GetDecryptedPassword(@base.Password, MasterPassword);
			Clipboard.SetText(encryptedPassword.ResultData);
			@base.StartClipboardTimer(currentText);
		}
		private async void OnRemoveRemoteConnection(RemoteAdressData data)
		{
			TextInputDialog textInputDialog = new();
			textInputDialog.Title = "Löschen Bestätigen";
			textInputDialog.Text = "Gib den Namen des Tresors ein um die Verbindung zu entfernen";
			var result = await _contentDialogService.ShowAsync(new ContentDialog()
			{
				Content = textInputDialog,
				PrimaryButtonText = "OK",
			}, new CancellationToken());
			if(result.ToString() != "Primary") return;
			if(result.ToString() == "Primary" && textInputDialog.Input != data.PersonalName)
			{
				_snackbarService.Show("Fehler", "Der Name des Tresors ist nicht korrekt", Wpf.Ui.Controls.ControlAppearance.Caution, null, new TimeSpan(0, 0, 5));
				return;
			}
			_snackbarService.Show("Gelöscht", "Die Verbindung zu dem Tresor wurde entfernt", Wpf.Ui.Controls.ControlAppearance.Success, null, new TimeSpan(0, 0, 5));
			_configurationService.RemoveRemoteAddress(data.FilePath);
			Addresses.Remove(data);
		}

		private void OnCopyUsername(PasswordBase @base)
		{
			_snackbarService.Show("Kopiert", "Der Benutzername wurde in die Zwischenablage kopiert", Wpf.Ui.Controls.ControlAppearance.Success, null, new TimeSpan(0, 0, 5));
			Clipboard.SetText(@base.Username);
		}

		private void OnDeletePasswordEntry(PasswordBase @base)
		{
			_snackbarService.Show("Gelöscht", "Das Passwort wurde aus deinem Tresor gelöscht", Wpf.Ui.Controls.ControlAppearance.Caution, null, new TimeSpan(0, 0, 5));
			Passwords.Remove(@base);
			_passwordService.DeletePassword(@base);
			OnPropertyChanged(nameof(Passwords));
		}

		private void OnEditPasswordEntry(PasswordBase @base)
		{
			var encodedPassword = _passwordService.GetDecryptedPassword(@base.Password, MasterPassword);
			PasswordBase passwordBase = new()
			{
				Name = @base.Name,
				Note = @base.Note,
				Password = encodedPassword.ResultData,
				PasswordEntryDesign = @base.PasswordEntryDesign,
				PasswordProperties = @base.PasswordProperties,
				PasswordType = @base.PasswordType,
				StorageType = @base.StorageType,
				Topic = @base.Topic,
				PasswordScore = @base.PasswordScore,
				Username = @base.Username,
				Value = @base.Value,
				Created = @base.Created,
				Guid = @base.Guid
			};
			_editPassword.SetPasswordBase(passwordBase);
			SetModuleView(_editPassword);
		}
		private void OnAddSource()
		{
			SetModuleView(_createSource);
		}
		private void OnOpenPasswordBackup(PasswordBase @base)
		{
			_backupList.SetPasswordBackup(@base.passwordBackups, @base);
			SetModuleView(_backupList);
		}

		private void OnPasswordChanged(object? sender, PasswordBase e)
		{
			var decodedPassword = _passwordService.GetEncryptedPassword(e.Password, MasterPassword);
			e.Password = decodedPassword.ResultData;
			_passwordService.ChangePassword(e, e.PasswordChangeComment);
			/* Update on UI */
			var password = Passwords.Where(x => x.Created == e.Created).FirstOrDefault();
			if (password != null)
			{
				password = e;
				password.DataChanged();
				LoadPasswordsForSelectedTopic();
			}
			CloseModuleView();
		}
		private void OnPasswordCreated(object? sender, PasswordBase e)
		{
			var hashedPassword = _passwordService.GetEncryptedPassword(e.Password, MasterPassword);
			var topic = SelectedTopic.Name;
			e.Topic = topic;
			var password = new PasswordBase()
			{
				Name = e.Name,
				Note = e.Note,
				Password = hashedPassword.ResultData,
				PasswordEntryDesign = e.PasswordEntryDesign,
				PasswordProperties = e.PasswordProperties,
				PasswordType = e.PasswordType,
				StorageType = e.StorageType,
				Topic = e.Topic,
				PasswordScore = e.PasswordScore,
				Username = e.Username,
				Value = e.Value,
				Created = e.Created
			};
			_passwordService.AddPassword(password);
			Passwords.Add(password);
			OnPropertyChanged(nameof(Passwords));
		}

		private void OnTopicCreated(object? sender, Topic e)
		{
			Topics.Add(new Topic()
			{
				Description = e.Description,
				Name = e.Name,
				Design = e.Design
			});
			CloseModuleView();
			OnPropertyChanged(nameof(Topics));
		}
		private void OpenCreatePassword() => SetModuleView(_createPasswordView);

		private void OpenCreateTopic() => SetModuleView(_createTopicView);
		private void SetModuleView(Control control)
		{
			var storyboard = _view.FindResource("ExpandModuleView") as Storyboard;
			storyboard.Begin();
			ModuleView = control;
			OnPropertyChanged(nameof(ModuleView));
		}

		#endregion Private Methods
	}
}

