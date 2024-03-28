using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Configurations;
using CipherKey.Core.Data;
using CipherKey.Core.Enums;
using CipherKey.Core.Extensions;
using CipherKey.Core.Helpers;
using CipherKey.Core.Logging;
using CipherKey.Core.Models;
using CipherKey.Core.Password;
using CipherKey.Crypt;
using CipherKey.Services.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Services.Configuration
{
	public class ConfigurationService : IConfigurationService
	{

        #region Public Constructors

        private XmlService<PublicApplicationConfiguration> _configuration;
        private readonly ILogService _logService;
		private readonly IPasswordService _passwordService;
        public ConfigurationService(ILogService logService, IPasswordService passwordService)
		{
            _logService = logService;
			_passwordService = passwordService;
			_configuration =
				new XmlService<PublicApplicationConfiguration>(FilePaths.CipherPublicConfigurationFilePath);
		}

		#endregion Public Constructors

		#region Public Methods

		public CipherResult<bool> AddTopic(Topic topic)
        {
            try
            {
				var t = CipherF<CipherStorage>.Load();
				t.Topics.Add(topic);
				CipherF<CipherStorage>.Save(t);
                _logService.Info<IConfigurationService>("Topic added", topic);
				return new CipherResult<bool> { ResultData = true };
			}
            catch(Exception e)
            {
                _logService.Error<IConfigurationService>("Failed to add topic", topic, e);
                return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
            }
        }

        public CipherResult<string> CheckPassword(string password)
        {
            try
            {
				var t = CipherF<CipherStorage>.Load();
				if (t.ApplicationConfiguration.MasterPassword == password.Hash())
				{
					return new CipherResult<string> { ResultData = password };
				}
                _logService.Info<IConfigurationService>("Password entered and checked successfully", password);
				return new CipherResult<string> { ResultData = string.Empty };
			}
            catch(Exception e)
            {
				_logService.Error<IConfigurationService>("Failed to check password", password, e);
				return new CipherResult<string> { ResultData = string.Empty, ErrorText = e.Message };
			}
        }

        public CipherResult<bool> DeleteTopic(Topic topic)
        {
            try
            {
				var t = CipherF<CipherStorage>.Load();
				t.Topics.Remove(topic);
				CipherF<CipherStorage>.Save(t);
                _logService.Info<IConfigurationService>("Topic deleted", topic);
				return new CipherResult<bool> { ResultData = true };
			}
            catch(Exception e)
            {
                _logService.Error<IConfigurationService>("Failed to delete topic", topic, e);
				return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
            }
        }

		public CipherResult<List<Topic>> GetTopics()
        {
            try
            {
				CipherF<CipherStorage>.Load();
                _logService.Info<IConfigurationService>("Topics loaded successfully", null);
				return new CipherResult<List<Topic>> { ResultData = CipherF<CipherStorage>.Load().Topics };
			}
            catch(Exception e)
            {
                _logService.Error<IConfigurationService>("Failed to load topics", null, e);
				return new CipherResult<List<Topic>> { ResultData = new List<Topic>(), ErrorText = e.Message };
            }
        }

        public void Initialize()
        {
            _logService.Info<IConfigurationService>("Initialize Configuration Service", null);
			FilePaths.CreateBaseFilePath();
			SetApplicationConfiguration();
            _logService.Info<IConfigurationService>("Configuration Service initialized", null);
		}
        public CipherResult<bool> IsConfigured()
        {
            try
            {
                var cipherData = CipherF<CipherStorage>.Load();
                var applicationConfiguration = cipherData.ApplicationConfiguration;
                if (applicationConfiguration != null && !string.IsNullOrEmpty(applicationConfiguration.MasterPassword))
                {
                    _logService.Info<IConfigurationService>("Application is configured", null);
                    return new CipherResult<bool> { ResultData = true };
                }
                _logService.Info<IConfigurationService>("Application is not configured", null);
                return new CipherResult<bool> { ResultData = false };
            }
            catch (Exception e)
            {
                _logService.Error<IConfigurationService>("Failed to check if application is configured", null, e);
                return new CipherResult<bool> { ResultData = false };
            }
        }

		public CipherResult<bool> SetMasterPassword(string enteredMasterPassword)
        {
            try
            {
				var f = CipherF<CipherStorage>.Load();
				f.ApplicationConfiguration.MasterPassword = enteredMasterPassword.Hash();
				CipherF<CipherStorage>.Save(f);
                _logService.Info<IConfigurationService>("Master password set", null);
				return new CipherResult<bool> { ResultData = true };
			}
            catch(Exception e)
            {
                _logService.Error<IConfigurationService>("Failed to set master password", null, e);
                return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
            }
        }

        public CipherResult<bool> UpdateTopic(Topic topic)
        {
            try
            {
				var t = CipherF<CipherStorage>.Load();
				var oldTopic = t.Topics.FirstOrDefault(x => x.Name == topic.Name);
				if (oldTopic != null)
				{
					t.Topics.Remove(oldTopic);
					t.Topics.Add(topic);
					CipherF<CipherStorage>.Save(t);
				}
				CipherF<CipherStorage>.Save(t);
                _logService.Info<IConfigurationService>("Topic updated", topic);
				return new CipherResult<bool> { ResultData = true };
			}
            catch(Exception e)
            {
                _logService.Error<IConfigurationService>("Failed to update topic", topic, e);
				return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
            }
        }
		#endregion Public Methods

		#region Private Methods

		private void SetApplicationConfiguration()
        {
            _logService.Info<IConfigurationService>("Set Application Configuration", null);
			if (!IsConfigured().ResultData)
			{
				var t = CipherF<CipherStorage>.Load();
				t = new CipherStorage();
				t.Topics.Add(new Topic()
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
				CipherF<CipherStorage>.Save(t);
                _logService.Info<IConfigurationService>("Application Configuration set", null);
			}
        }

		public CipherResult<bool> AddTopic(Topic topic, ISourceConfiguration sourceConfiguration)
		{
			try
			{
				var blankPassword = _passwordService.GetDecryptedPassword(sourceConfiguration.Password, sourceConfiguration.LocalMasterPassword);
				var t = CipherF<CipherStorage>.Load(sourceConfiguration.Path, blankPassword.ResultData.Hash());
				t.Topics.Add(topic);
				CipherF<CipherStorage>.Save(sourceConfiguration.Path, blankPassword.ResultData.Hash(), t);
				_logService.Info<IConfigurationService>($"Topic added to Remote [{sourceConfiguration.Path}]", topic);
				return new CipherResult<bool> { ResultData = true };
			}
			catch (Exception e)
			{
				_logService.Error<IConfigurationService>($"Failed to add topic to Remote [{sourceConfiguration.Path}]", topic, e);
				return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
			}
		}

		public CipherResult<bool> DeleteTopic(Topic topic, ISourceConfiguration sourceConfiguration)
		{
			try
			{
				var t = CipherF<CipherStorage>.Load(sourceConfiguration.Path, sourceConfiguration.Password);
				t.Topics.Remove(topic);
				CipherF<CipherStorage>.Save(sourceConfiguration.Path, sourceConfiguration.Password, t);
				_logService.Info<IConfigurationService>($"Topic deleted at Remote [{sourceConfiguration.Path}]", topic);
				return new CipherResult<bool> { ResultData = true };
			}
			catch (Exception e)
			{
				_logService.Error<IConfigurationService>($"Failed to delete topic at Remote [{sourceConfiguration.Password}]", topic, e);
				return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
			}
		}

		public CipherResult<bool> UpdateTopic(Topic topic, ISourceConfiguration sourceConfiguration)
		{
			try
			{
				var t = CipherF<CipherStorage>.Load(sourceConfiguration.Path, sourceConfiguration.Password);
				var oldTopic = t.Topics.FirstOrDefault(x => x.Name == topic.Name);
				if (oldTopic != null)
				{
					t.Topics.Remove(oldTopic);
					t.Topics.Add(topic);
					CipherF<CipherStorage>.Save(t);
				}
				CipherF<CipherStorage>.Save(sourceConfiguration.Path, sourceConfiguration.Password, t);
				_logService.Info<IConfigurationService>($"Topic updated at Remote [{sourceConfiguration.Path}]", topic);
				return new CipherResult<bool> { ResultData = true };
			}
			catch (Exception e)
			{
				_logService.Error<IConfigurationService>($"Failed to update topic at Remote [{sourceConfiguration.Path}]", topic, e);
				return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
			}
		}

		public CipherResult<List<Topic>> GetTopics(ISourceConfiguration sourceConfiguration)
		{
			try
			{
				CipherF<CipherStorage>.Load(sourceConfiguration.Path, sourceConfiguration.Password);
				_logService.Info<IConfigurationService>($"Topics loaded successfully at Remote [{sourceConfiguration.Path}]", null);
				return new CipherResult<List<Topic>> { ResultData = CipherF<CipherStorage>.Load().Topics };
			}
			catch (Exception e)
			{
				_logService.Error<IConfigurationService>($"Failed to load topics at Remote [{sourceConfiguration.Path}]", null, e);
				return new CipherResult<List<Topic>> { ResultData = new List<Topic>(), ErrorText = e.Message };
			}
		}

		public CipherResult<bool> UpdateApplicationConfiguration(PublicApplicationConfiguration updateValues)
		{
			Predicate<PublicApplicationConfiguration> get = (conf) => conf.ID == updateValues.ID;
			Action<PublicApplicationConfiguration> upd = (update) => update = updateValues;
			_configuration.Update(get, upd);
			return new CipherResult<bool> { ResultData = true };
		}

		public CipherResult<PublicApplicationConfiguration> GetApplicationConfiguration()
		{
			var result = _configuration.GetAll();
			return new CipherResult<PublicApplicationConfiguration>() { ResultData = result.First() };
		}

		#endregion Private Methods
	}
}
