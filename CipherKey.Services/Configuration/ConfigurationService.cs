using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Configurations;
using CipherKey.Core.Extensions;
using CipherKey.Core.Helpers;
using CipherKey.Core.Models;
using CipherKey.Core.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Services.Configuration
{
	public class ConfigurationService : IConfigurationService
	{
		private XmlService<ApplicationConfiguration> _applicationConfigurationService;
		private XmlService<PasswordBase> _passwordService;
		private XmlService<Topic> _topicService;
		public ConfigurationService()
		{
			FilePaths.CreateBaseFilePath();
			_topicService = new XmlService<Topic>(FilePaths.PasswordStorageFilePath + FilePaths.TopicFileName);
			_passwordService = new XmlService<PasswordBase>(FilePaths.PasswordStorageFilePath + FilePaths.PasswordsFileName);
			_applicationConfigurationService = new XmlService<ApplicationConfiguration>(FilePaths.PasswordStorageFilePath + FilePaths.ApplicationConfigurationFileName);
			SetApplicationConfiguration();
		}
		private void SetApplicationConfiguration()
		{
			if (!IsConfigured().ResultData)
			{
				_applicationConfigurationService.Add(new ApplicationConfiguration());
				_topicService.Add(new Topic()
				{
					Name = "Allgemein",
					Description = "Allgemeine Passwörter",
					Design = new TopicEntryDesign()
					{
						BackgroundHex = "FF0000",
						BorderHex = "#FF0000",
						ForegroundHex = "#575757",
					}
				});
			}
		}

		public CipherResult<bool> ChangeMasterPassword(string oldMasterPassword, string newMasterPassword)
		{
			throw new NotImplementedException();
		}

		public CipherResult<bool> GetDecodedPasswords<T>(T entry, string masterPassword)
		{
			throw new NotImplementedException();
		}

		public CipherResult<bool> IsConfigured()
		{
			var applicationConfiguration = _applicationConfigurationService.GetAll();
			if (applicationConfiguration.Count > 0)
			{
				var firstElement = applicationConfiguration.FirstOrDefault();
				if (firstElement != null && !string.IsNullOrEmpty(firstElement.MasterPassword))
				{
					return new CipherResult<bool> { ResultData = true };
				}
			}
			return new CipherResult<bool> { ResultData = false };
		}

		public CipherResult<bool> SetMasterPassword(string enteredMasterPassword)
		{
			Predicate<ApplicationConfiguration> predicate = (x) => string.IsNullOrEmpty(x.MasterPassword);
			Action<ApplicationConfiguration> action = (x) => x.MasterPassword = enteredMasterPassword.Hash();
			_applicationConfigurationService.Update(predicate, action);
			return new CipherResult<bool> { ResultData = true };
		}

		public CipherResult<string> CheckPassword(string password)
		{
			var applicationConfiguration = _applicationConfigurationService.GetAll();
			if (applicationConfiguration.Count > 0)
			{
				var firstElement = applicationConfiguration.FirstOrDefault();
				if (firstElement != null && firstElement.MasterPassword == password.Hash())
				{
					return new CipherResult<string> { ResultData = password };
				}
			}
			return new CipherResult<string> { ResultData = string.Empty };
		}

		public CipherResult<bool> AddTopic(Topic topic)
		{
			_topicService.Add(topic);
			return new CipherResult<bool> { ResultData = true };
		}

		public CipherResult<bool> DeleteTopic(Topic topic)
		{
			Predicate<Topic> predicate = (x) => x.Name == topic.Name;
			_topicService.Delete(predicate);
			return new CipherResult<bool> { ResultData = true };
		}

		public CipherResult<bool> UpdateTopic(Topic topic)
		{
			Predicate<Topic> predicate = (x) => x.Name == topic.Name;
			Action<Topic> action = (x) => x = topic;
			_topicService.Update(predicate, action);
			return new CipherResult<bool> { ResultData = true };
		}

		public CipherResult<List<Topic>> GetTopics()
		{
			return new CipherResult<List<Topic>> { ResultData = _topicService.GetAll() };
		}
	}
}
