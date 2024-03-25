using CipherKey.Core.Data;
using CipherKey.Core.Events;
using CipherKey.Core.Helpers;
using CipherKey.Core.Password;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace CipherKey.Core.UserControls
{
	/// <summary>
	/// Interaction logic for PasswordBackupList.xaml
	/// </summary>
	public partial class PasswordBackupList : UserControl, INotifyPropertyChanged
    {
		public EventHandler<PasswordBackupGeneratedEvent> PasswordBackupSetEvent;
		public PasswordBase PasswordBase { get; set; }
        public ObservableCollection<PasswordBackupData> Backups { get; set; } = new ObservableCollection<PasswordBackupData>();
		public IDelegateCommand ExecuteSetPasswordCommand => new DelegateCommand<PasswordBackupData>(SetPassword);

		public PasswordBackupList()
        {
            InitializeComponent();
            DataContext = this;
        }

		private void SetPassword(PasswordBackupData data)
		{
			PasswordBackupSetEvent?.Invoke(this, new PasswordBackupGeneratedEvent()
			{
				PasswordBackupData = data,
				PasswordBase = PasswordBase
			});
		}

		public void SetPasswordBackup(List<PasswordBackupData> backups, PasswordBase @base)
		{
			PasswordBase = @base;
			Backups = new ObservableCollection<PasswordBackupData>(backups.OrderByDescending(x => x.ChangedAt));
			OnPropertyChanged(nameof(Backups));
		}

		public event PropertyChangedEventHandler? PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
