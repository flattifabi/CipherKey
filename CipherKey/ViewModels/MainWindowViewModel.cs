using CipherKey.Core.Configurations;
using CipherKey.Core.Helpers;
using CipherKey.Core.Password;
using CipherKey.Core.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CipherKey.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<Topic> Topics { get; set; }
        public ObservableCollection<PasswordBase> Passwords { get; set; }
        private Topic _selectedTopic;
        private Control _moduleView;
		public IDelegateCommand CloseCommand => new DelegateCommand(Close);
        public IDelegateCommand CreateTopicCommand => new DelegateCommand(OpenCreateTopic);
        public IDelegateCommand CreatePasswordCommand => new DelegateCommand(OpenCreatePassword);
		public IDelegateCommand CopyPassword => new DelegateCommand<PasswordBase>(OnCopyPassword);

		private readonly IConfigurationService _configurationService;
        private readonly IPasswordService _passwordService;
        private readonly MainWindow _view;
        private readonly CreateTopic _createTopicView;
        private readonly CreatePassword _createPasswordView;
		public string MasterPassword { get; set; } = string.Empty;
        public MainWindowViewModel(IConfigurationService configurationService, IPasswordService passwordService, MainWindow view, CreateTopic createTopicView,
            CreatePassword createPasswordView)
        {
            _configurationService = configurationService;
            _view = view;
            _createTopicView = createTopicView;
            _passwordService = passwordService;
            _createPasswordView = createPasswordView;
        }
        public void Initialize()
        {
            Topics = new ObservableCollection<Topic>(_configurationService.GetTopics().ResultData);
            _createTopicView.TopicCreated += OnTopicCreated;
			_createPasswordView.PasswordCreated += OnPasswordCreated;
			OnPropertyChanged(nameof(Topics));
        }
		private void OnCopyPassword(PasswordBase @base)
		{
            var currentText = Clipboard.GetText();
            var encryptedPassword = _passwordService.GetDecryptedPassword(@base.Password, MasterPassword);
            Clipboard.SetText(encryptedPassword.ResultData);
            @base.StartClipboardTimer(currentText);
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

		private void OpenCreateTopic() => SetModuleView(_createTopicView);
        private void OpenCreatePassword() => SetModuleView(_createPasswordView);
		private void SetModuleView(Control control)
        {
			var storyboard = _view.FindResource("ExpandModuleView") as Storyboard;
			storyboard.Begin();
			ModuleView = control;
            OnPropertyChanged(nameof(ModuleView));
		}
        private void CloseModuleView()
        {
            var storyboard = _view.FindResource("CollapseModuleView") as Storyboard;
            storyboard.Begin();
        }
        private void LoadPasswordsForSelectedTopic()
        {
            Passwords = new ObservableCollection<PasswordBase>(_passwordService.GetPasswordsByTopic(SelectedTopic.Name).ResultData);
            OnPropertyChanged(nameof(Passwords));
        }
		private void Close()
		{
			Application.Current.Shutdown();
		}
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
        public Control ModuleView
        {
            get => _moduleView;
            set
            {
                _moduleView = value;
                OnPropertyChanged(nameof(ModuleView));
            }
        }
	}
}
