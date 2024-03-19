using CipherKey.Core.Enums;
using CipherKey.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace CipherKey.Core.Password
{
	public class PasswordBase : BaseViewModel
    {
        private bool _isEditOn;
        [XmlIgnore]
        public bool IsEditOn
        {
            get => _isEditOn;
            set
            {
                _isEditOn = value;
                OnPropertyChanged(nameof(IsEditOn));
            }
        }
        [XmlIgnore]
        public bool IsCopyEnabled { get; set; } = true;
        [XmlIgnore]
        private string _oldText = string.Empty;
		[XmlIgnore]
		public DispatcherTimer _clipboardTimer = new DispatcherTimer();
        [XmlIgnore]
        public Nullable<int> AvailableSeconds { get; set; } = null;
        public void DataChanged()
        {
            OnPropertyChanged(nameof(Name), nameof(Note), nameof(Password), nameof(PasswordEntryDesign), nameof(PasswordProperties), 
                nameof(PasswordScore), nameof(PasswordType), nameof(StorageType), nameof(Topic), nameof(Username), nameof(Value));
        }
		public void StartClipboardTimer(string oldText)
		{
            IsCopyEnabled = false;
			AvailableSeconds = 25;
            _oldText = oldText;
			_clipboardTimer.Interval = TimeSpan.FromSeconds(1);
			_clipboardTimer.Tick += ClipboardTimer_Tick;
            OnPropertyChanged(nameof(IsCopyEnabled));
			Task.Run(() =>
			{
				_clipboardTimer.Start();
			});
		}

		private void ClipboardTimer_Tick(object? sender, EventArgs e)
		{
			AvailableSeconds = AvailableSeconds -1;
            OnPropertyChanged(nameof(AvailableSeconds));
			if (AvailableSeconds <= 0)
			{
                Clipboard.SetText(_oldText);
                AvailableSeconds = null;
                _clipboardTimer.Stop();
                IsCopyEnabled = true;
                OnPropertyChanged(nameof(IsCopyEnabled));
			}
		}

		[XmlAttribute]
        public PasswordType PasswordType { get; set; } = PasswordType.Other;
        [XmlAttribute]
        public string Topic { get; set; } = "General";
        [XmlAttribute]
        public string Value { get; set; } = string.Empty;
        [XmlAttribute]
        public string Note { get; set; } = string.Empty;
        [XmlAttribute]
        public string Username { get; set; } = string.Empty;
        [XmlAttribute]
        public string Password { get; set; } = string.Empty;
        [XmlAttribute]
        public string Name { get; set; } = string.Empty;
        [XmlAttribute]
        public int PasswordScore { get; set; }
        [XmlAttribute]
        public DateTime Created { get; set; } = DateTime.Now;
        [XmlAttribute]
        public StorageType StorageType { get; set; } = StorageType.Local;
        public PasswordEntryDesign PasswordEntryDesign { get; set; } = new PasswordEntryDesign();
        public PasswordProperties PasswordProperties { get; set; } = new PasswordProperties();
    }
}
