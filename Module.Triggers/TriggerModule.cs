using System.Windows.Controls;
using CipherKey.Core.Interfaces;
using Wpf.Ui.Controls;

namespace Module.Triggers;

public class TriggerModule : IModuleBase
{
    public string ModuleName => "Trigger";
    public Control View { get; set; }
    public SymbolIcon Symbol { get; set; } = new SymbolIcon(SymbolRegular.Building16, fontSize: 30);

    private readonly TriggerModuleView _view;
    private readonly TriggerModuleViewModel _viewModel;
    public TriggerModule(TriggerModuleView view, TriggerModuleViewModel viewModel)
    {
        _view = view;
        _viewModel = viewModel;
    }
    public void Initialize()
    {
        _viewModel.Initialize();
        _view.DataContext = _viewModel;
        View = _view;
    }
}