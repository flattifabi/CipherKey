using System.Windows.Controls;
using CipherKey.Core.Interfaces;
using Wpf.Ui.Controls;

namespace Module.FileCryptor;

public class FileCryptorModule : IModuleBase
{
    public string ModuleName { get; }
    public Control View { get; set; }
    public SymbolIcon Symbol { get; set; }
    public void Initialize()
    {
        
    }
}