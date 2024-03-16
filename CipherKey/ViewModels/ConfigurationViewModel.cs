using CipherKey.Core.Configurations;
using CipherKey.Core.Helpers;
using CipherKey.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CipherKey.ViewModels
{
	public class ConfigurationViewModel : BaseViewModel
	{
		private readonly IConfigurationService _configurationService;
		private readonly ConfigurationWindow _view;
		public event Action ConfigurationFinished = delegate { };

		public IDelegateCommand CreateCommand => new DelegateCommand(Create);
		public IDelegateCommand CloseCommand => new DelegateCommand(Close);

		public ConfigurationViewModel(IConfigurationService configurationService, ConfigurationWindow view)
		{
			_configurationService = configurationService;
			_view = view;
		}
		private void Create()
		{
			if(_view.password.Password == _view.repeatPassword.Password)
			{
				var result = _configurationService.SetMasterPassword(_view.password.Password);
				if (result.ResultData)
					ConfigurationFinished?.Invoke();
				else return; /* There should be an error */
            }
            else
			{
				/* 2 different passwords entered */
			}
		}
		private void Close()
		{
			Application.Current.Shutdown();
		}
	}
}
