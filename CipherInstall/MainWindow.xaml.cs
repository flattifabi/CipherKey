using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CipherKey.Core.Helpers;
using Wpf.Ui.Controls;

namespace CipherInstall;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow, INotifyPropertyChanged
{
    private bool _createDesktopShortCut;
    private bool _sendAnalytics;
    private bool _isInstallActive;
    private string _installInformation;
    public bool IsInstallActive
    {
        get => _isInstallActive;
        set
        {
            _isInstallActive = IsArrangeValid;
            OnPropertyChanged();
        }
    }

    public bool SendAnalytics
    {
        get => _sendAnalytics;
        set
        {
            _sendAnalytics = value;
            OnPropertyChanged();
        }
    }
    public bool CreateDesktopShortcut
    {
        get => _createDesktopShortCut;
        set
        {
            _createDesktopShortCut = value;
            OnPropertyChanged();
        }
    }
    public string InstallInformation
    {
        get => _installInformation;
        set
        {
            _installInformation = value;
            OnPropertyChanged();
        }
    }

    public IDelegateCommand StartInstallCommand => new DelegateCommand(StartInstall);
    

    public IDelegateCommand CloseCommand => new DelegateCommand(new Action(() => Application.Current.Shutdown()));
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void StartInstall()
    {
        
    }
    
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}