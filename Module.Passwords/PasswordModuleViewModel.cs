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

namespace Module.Passwords
{
	public class PasswordModuleViewModel : BaseViewModel
	{
		#region Fields

		private readonly IConfigurationService _configurationService;
		private readonly CreatePassword _createPasswordView;
		private readonly CreateTopic _createTopicView;
		private readonly EditPassword _editPassword;
		private readonly IPasswordService _passwordService;
		private readonly ISnackbarService _snackbarService;
		private readonly PasswordModuleView _view;
		private Control _moduleView;
		private Topic _selectedTopic;

		#endregion Fields


		#region Public Constructors

		public PasswordModuleViewModel(IConfigurationService configurationService, IPasswordService passwordService, ISnackbarService snackbarService,
			PasswordModuleView view, CreateTopic createTopicView, CreatePassword createPasswordView, EditPassword editPassword)
		{
			_configurationService = configurationService;
			_view = view;
			_createTopicView = createTopicView;
			_passwordService = passwordService;
			_createPasswordView = createPasswordView;
			_snackbarService = snackbarService;
			_editPassword = editPassword;
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

		public void Initialize()
		{
			Topics = new ObservableCollection<Topic>(_configurationService.GetTopics().ResultData);
			_createTopicView.TopicCreated += OnTopicCreated;
			_createPasswordView.PasswordCreated += OnPasswordCreated;
			_editPassword.PasswordChanged += OnPasswordChanged;
			SelectedTopic = Topics.FirstOrDefault();
			OnPropertyChanged(nameof(Topics));
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
				Created = @base.Created
			};
			_editPassword.SetPasswordBase(passwordBase);
			SetModuleView(_editPassword);
		}

		private void OnPasswordChanged(object? sender, PasswordBase e)
		{
			var decodedPassword = _passwordService.GetEncryptedPassword(e.Password, MasterPassword);
			e.Password = decodedPassword.ResultData;
			_passwordService.ChangePassword(e);
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

