using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Data;
using CipherKey.Core.Helpers;
using CipherKey.Core.Logging;
using CipherKey.Core.Models;
using CipherKey.Core.Password;
using CipherKey.Crypt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Services.Password
{
    public class PasswordService : IPasswordService
	{

        #region Public Constructors
        private readonly ILogService _logService;
        public PasswordService(ILogService logService)
		{
            _logService = logService;
        }
        #endregion Public Constructors

        #region Public Methods

        public CipherResult<bool> AddPassword(PasswordBase password)
        {
            try
            {
				var f = CipherF<CipherStorage>.Load();
				f.Passwords.Add(password);
				CipherF<CipherStorage>.Save(f);
                _logService.Info<IPasswordService>("Password added", null);
				return new CipherResult<bool> { ResultData = true };
			}
            catch(Exception e)
            {
                _logService.Error<IPasswordService>("Failed to add password", null, e);
				return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
            }
        }

        public CipherResult<bool> ChangePassword(PasswordBase password, string? comment = "")
        {
            try
            {
				var t = CipherF<CipherStorage>.Load();
				var oldPassword = t.Passwords.FirstOrDefault(x => x.Guid == password.Guid);
				if (oldPassword != null)
				{
					t.Passwords.Remove(oldPassword);
					t.Passwords.Add(password);
					t = AddPasswordBackup(oldPassword, t, comment);
					CipherF<CipherStorage>.Save(t);
				}
                _logService.Info<IPasswordService>("Password changed", null);
				return new CipherResult<bool> { ResultData = true };
			}
            catch(Exception e)
            {
                _logService.Error<IPasswordService>("Failed to change password", null, e);
                return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
            }
          
        }

        public CipherResult<bool> DeletePassword(PasswordBase password)
        {
            try
            {
				var f = CipherF<CipherStorage>.Load();
				var foundPassword = f.Passwords.FirstOrDefault(x => x.Created == password.Created);
				if (foundPassword != null)
				{
					f.Passwords.Remove(foundPassword);
					CipherF<CipherStorage>.Save(f);
				}
                _logService.Info<IPasswordService>("Password deleted", null);
				return new CipherResult<bool> { ResultData = true };
			}
            catch(Exception e)
            {
                _logService.Error<IPasswordService>("Failed to delete password", null, e);
				return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
            }
        }

        public CipherResult<string> GetDecryptedPassword(string value, string masterPassword)
        {
            var result = new CipherResult<string>();

            try
            {
                _logService.Debug<IPasswordService>($"Decrypting password {DateTime.Now}", null);
                int iterationCount = 10000;
                using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(masterPassword, new byte[16], iterationCount))
                {
                    byte[] key = pbkdf2.GetBytes(32);
                    byte[] iv = pbkdf2.GetBytes(16);
                    using (RijndaelManaged aesAlg = new RijndaelManaged())
                    {
                        aesAlg.Key = key;
                        aesAlg.IV = iv;
                        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(value)))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                {
                                    _logService.Debug<IPasswordService>($"Password decrypted {DateTime.Now}", null);
                                    return new CipherResult<string> { ResultData = srDecrypt.ReadToEnd() };
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logService.Error<IPasswordService>("Failed to decrypt password", null, ex);
                return new CipherResult<string> { ErrorText = ex.Message, ErrorCode = -999 };
            }
        }

        public CipherResult<string> GetEncryptedPassword(string value, string masterPassword)
        {
			var result = new CipherResult<string>();
			try
			{
                _logService.Debug<IPasswordService>($"Encrypting password {DateTime.Now}", null);
				int iterationCount = 10000;
				using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(masterPassword, new byte[16], iterationCount))
				{
					byte[] key = pbkdf2.GetBytes(32);
					byte[] iv = pbkdf2.GetBytes(16);
					using (RijndaelManaged aesAlg = new RijndaelManaged())
					{
						aesAlg.Key = key;
						aesAlg.IV = iv;
						ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
						using (MemoryStream msEncrypt = new MemoryStream())
						{
							using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
							{
								using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
								{
									swEncrypt.Write(value);
								}
                                _logService.Debug<IPasswordService>($"Password encrypted {DateTime.Now}", null);
								return new CipherResult<string> { ResultData = Convert.ToBase64String(msEncrypt.ToArray()) };
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
                _logService.Error<IPasswordService>("Failed to encrypt password", null, ex);
				return new CipherResult<string> { ErrorText = ex.Message, ErrorCode = -999 };
			}
		}
        public CipherResult<List<PasswordBackupData>> GetPasswordBackups()
        {
            try
            {
				var t = CipherF<CipherStorage>.Load();
				var passwordBackups = t.PasswordBackUps ?? new List<PasswordBackupData>();
                _logService.Info<IPasswordService>("Password backups loaded successfully", null);
				return new CipherResult<List<PasswordBackupData>> { ResultData = passwordBackups };
			}
            catch(Exception e)
            {
                _logService.Error<IPasswordService>("Failed to get password backups", null, e);
                return new CipherResult<List<PasswordBackupData>> { ResultData = new List<PasswordBackupData>(), ErrorText = e.Message };
            }
        }

        public CipherResult<List<PasswordBase>> GetPasswordsByTopic(string topic)
        {
            try
            {
				var f = CipherF<CipherStorage>.Load();
				var passwords = f.Passwords.Where(x => x.Topic.ToLower() == topic.ToLower()).ToList();
				foreach (var password in passwords)
				{
					var foundPasswordBackup = f.PasswordBackUps.Where(x => x.PasswordData.Created == password.Created).ToList();
					password.passwordBackups = foundPasswordBackup;
				}
                _logService.Info<IPasswordService>("Passwords loaded by topic successfully", null);
				return new CipherResult<List<PasswordBase>> { ResultData = passwords };
			}
            catch(Exception e)
            {
                _logService.Error<IPasswordService>("Failed to get passwords by topic", null, e);
				return new CipherResult<List<PasswordBase>> { ResultData = new List<PasswordBase>(), ErrorText = e.Message };
            }
        }

        public CipherResult<bool> RestorePasswordFromBackup(PasswordBase currentPassword, PasswordBackupData backupData)
        {
            try
            {
				var t = CipherF<CipherStorage>.Load();
				var foundPassword = t.Passwords.Where(x => x.Guid == currentPassword.Guid).FirstOrDefault();
				if (foundPassword == null)
					return new CipherResult<bool> { ResultData = false, ErrorText = "Password not found" };
				else
				{
					t.Passwords.Remove(foundPassword);
					t.Passwords.Add(backupData.PasswordData);
				}
				CipherF<CipherStorage>.Save(t);
                _logService.Info<IPasswordService>("Password restored from backup", null);
				return new CipherResult<bool> { ResultData = true };
			}
            catch(Exception e)
            {
                _logService.Error<IPasswordService>("Failed to restore password from backup", null, e);
                return new CipherResult<bool> { ResultData = false, ErrorText = e.Message };
            }
        }

        #endregion Public Methods

        #region Private Methods

        private CipherStorage AddPasswordBackup(PasswordBase passwordBase, CipherStorage cipherStorage, string? comment = "")
		{
            try
            {
				PasswordBackupData passwordBackupData = new PasswordBackupData() { PasswordData = passwordBase, ChangedAt = DateTime.Now, Comment = comment };
				cipherStorage.PasswordBackUps.Add(passwordBackupData);
                _logService.Info<IPasswordService>("Password backup added", null);
				return cipherStorage;
			}
            catch (Exception e)
            {
				_logService.Error<IPasswordService>("Failed to add password backup", null, e);
                return cipherStorage;
            }
        }

        #endregion Private Methods
    }
}
