using CipherKey.Core.Helpers;
using CipherKey.Core.Password;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace CipherKey.Core.Dialogs
{
	/// <summary>
	/// Interaktionslogik für AddRemoteTopicDialog.xaml
	/// </summary>
	public partial class AddRemoteTopicDialog : UserControl, INotifyPropertyChanged
	{
		private string _selectedForegroundColor;
		public List<string> AvailableCollors { get; set; } = new List<string>()
		{
			"#800080",
			"#FF00FF",
			"#000080",
			"#0000FF",
			"#008080",
			"#00FFFF",
			"#008000",
			"#00FF00",
			"#808000",
			"#FFFF00",
			"#800000",
			"#FF0000",
			"#808080",
			"#00000000"
		};
		public Topic Topic { get; set; } = new Topic();
		public IDelegateCommand SetIcon => new DelegateCommand(OpenIconDialog);
		public AddRemoteTopicDialog()
		{
			InitializeComponent();
			DataContext = this;
		}
		private void OpenIconDialog()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Bilddateien (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif|Alle Dateien (*.*)|*.*";
			openFileDialog.Title = "Bild auswählen";

			if (openFileDialog.ShowDialog() == true)
			{
				string selectedImagePath = openFileDialog.FileName;
				Topic.Design.TopicImage = selectedImagePath;
				OnPropertyChanged(nameof(Topic));
			}
		}
		public string SelectedForegroundColor
		{
			get => _selectedForegroundColor;
			set
			{
				_selectedForegroundColor = value;
				Topic.Design.ForegroundHex = value;
				OnPropertyChanged(nameof(SelectedForegroundColor));
			}
		}
		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
