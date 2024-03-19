using CipherKey.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace Module.Passwords
{
	public class PasswordModule : IModuleBase
	{
		public string ModuleName => "Passwort Tresor";
		public Control View { get; set; }
		public SymbolIcon Symbol { get; set; } = new SymbolIcon() { Symbol = SymbolRegular.Key32, FontSize = 25 };
		private PasswordModuleViewModel _viewModel;
		private PasswordModuleView _view;
		public PasswordModule(PasswordModuleView view, PasswordModuleViewModel viewModel)
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
}
