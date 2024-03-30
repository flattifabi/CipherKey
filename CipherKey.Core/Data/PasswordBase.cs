using CipherKey.Core.Data;
using CipherKey.Core.Enums;
using CipherKey.Core.Helpers;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;
using CipherKey.Core.Automation;

namespace CipherKey.Core.Password
{
	public class PasswordBase : BaseViewModel
    {
        #region Public Fields

        [XmlIgnore]
        public DispatcherTimer _clipboardTimer = new DispatcherTimer();

        #endregion Public Fields

        #region Private Fields

        private bool _isCopyActive;
        private bool _isEditOn;
        [XmlIgnore]
        private string _oldText = string.Empty;

        #endregion Private Fields

        #region Public Properties

        [XmlIgnore]
        public Nullable<int> AvailableSeconds { get; set; } = null;

        [XmlAttribute]
        public DateTime Created { get; set; } = DateTime.Now;

        [XmlIgnore]
        public bool IsCopyActive
        {
            get => _isCopyActive;
            set
            {
                _isCopyActive = value; OnPropertyChanged(nameof(IsCopyActive));
            }
        }
        [XmlIgnore]
        public string PasswordChangeComment { get; set; } = string.Empty;
        [XmlIgnore]
        public bool IsCopyEnabled { get; set; } = true;

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
        public List<PasswordBackupData> passwordBackups { get; set; } = new List<PasswordBackupData> { };
        [XmlAttribute]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute]
        public string Note { get; set; } = string.Empty;

        [XmlAttribute]
        public string Password { get; set; } = string.Empty;

        [XmlElement] public AutoTypeConfiguration AutoTypeConfiguration { get; set; } = new AutoTypeConfiguration() { };
        public PasswordEntryDesign PasswordEntryDesign { get; set; } = new PasswordEntryDesign();

        public PasswordProperties PasswordProperties { get; set; } = new PasswordProperties();

        [XmlAttribute]
        public int PasswordScore { get; set; }

        [XmlAttribute]
        public PasswordType PasswordType { get; set; } = PasswordType.Other;

        [XmlAttribute]
        public StorageType StorageType { get; set; } = StorageType.Local;

        [XmlAttribute]
        public string Topic { get; set; } = "General";

        [XmlAttribute]
        public string Username { get; set; } = string.Empty;

        [XmlAttribute]
        public string Value { get; set; } = string.Empty;
        [XmlAttribute]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [XmlIgnore]
        public bool IsAutoTypeAvailable
        {
	        get
	        {
		        if (string.IsNullOrEmpty(this.AutoTypeConfiguration.WebPath))
			        return false;
		        else return true;
	        }
        }

        #endregion Public Properties

        #region Public Methods
        
        public void DataChanged()
        {
            OnPropertyChanged(nameof(Name), nameof(Note), nameof(Password), nameof(PasswordEntryDesign), nameof(PasswordProperties), 
                nameof(PasswordScore), nameof(PasswordType), nameof(StorageType), nameof(Topic), nameof(Username), nameof(Value));
        }
		public void StartClipboardTimer(string oldText)
		{
            _clipboardTimer = new DispatcherTimer();
            IsCopyActive = true;
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

        #endregion Public Methods

        #region Private Methods

        private void ClipboardTimer_Tick(object? sender, EventArgs e)
		{
			AvailableSeconds = AvailableSeconds -1;
            OnPropertyChanged(nameof(AvailableSeconds));
			if (AvailableSeconds <= 0)
			{
                IsCopyActive = false;
                Clipboard.SetText(_oldText);
                AvailableSeconds = null;
                _clipboardTimer.Stop();
                _clipboardTimer = null;
                IsCopyEnabled = true;
                OnPropertyChanged(nameof(IsCopyEnabled));
			}
        }

        #endregion Private Methods
    }
}
