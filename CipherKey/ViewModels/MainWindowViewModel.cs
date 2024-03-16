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
		private readonly IConfigurationService _configurationService;
        private readonly MainWindow _view;
        private readonly CreateTopic _createTopicView;
		public string MasterPassword { get; set; } = string.Empty;
        public MainWindowViewModel(IConfigurationService configurationService, MainWindow view, CreateTopic createTopicView)
        {
            _configurationService = configurationService;
            _view = view;
            _createTopicView = createTopicView;
        }
        public void Initialize()
        {
            Topics = new ObservableCollection<Topic>(_configurationService.GetTopics().ResultData);
            _createTopicView.TopicCreated += OnTopicCreated;

			OnPropertyChanged(nameof(Topics));
        }

		private void OnTopicCreated(object? sender, Topic e)
		{
            Topics.Add(e);
            OnPropertyChanged(nameof(Topics));
		}

		private void OpenCreateTopic() => SetModuleView(_createTopicView);
        private void SetModuleView(Control control)
        {
			var storyboard = _view.FindResource("ExpandModuleView") as Storyboard;
			storyboard.Begin();
			ModuleView = control;
            OnPropertyChanged(nameof(ModuleView));
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
