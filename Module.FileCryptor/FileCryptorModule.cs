using System.Windows.Controls;
using CipherKey.Core.Interfaces;
using Wpf.Ui.Controls;

namespace Module.FileCryptor;

public class FileCryptorModule : IModuleBase
{
    public string ModuleName { get; }
    public Control View { get; set; }
    public SymbolIcon Symbol { get; set; }

    private readonly FileCryptorModuleView _view;
    private readonly FileCryptorModuleViewModel _viewModel;
    public FileCryptorModule(FileCryptorModuleViewModel viewModel, FileCryptorModuleView view)
    {
        _view = view;
        _viewModel = viewModel;
    }
    public void Initialize()
    {
        _view.DataContext = _viewModel;
        View = _view;
        _viewModel.Initialize();
    }
}