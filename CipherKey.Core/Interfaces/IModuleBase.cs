using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace CipherKey.Core.Interfaces
{
	public interface IModuleBase
	{
		string ModuleName { get; }
		public Control View { get; set; }
		public SymbolIcon Symbol { get; set; }
		void Initialize();
	}
}