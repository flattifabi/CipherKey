using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Helpers;
using CipherKey.Core.Models;
using CipherKey.Core.Password;
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
		private XmlService<PasswordBase> _passwordService;
		public PasswordService()
		{
			_passwordService = new XmlService<PasswordBase>(FilePaths.PasswordStorageFilePath + FilePaths.PasswordsFileName);
		}
		public CipherResult<string> GetEncryptedPassword(string value, string masterPassword)
		{
			var result = new CipherResult<string>();
			try
			{
				// Set iteration count for PBKDF2 (recommendation: at least 10000)
				int iterationCount = 10000;

				// Derive key using PBKDF2
				using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(masterPassword, new byte[16], iterationCount))
				{
					// Generate key and IV
					byte[] key = pbkdf2.GetBytes(32); // 256-bit key
					byte[] iv = pbkdf2.GetBytes(16);  // 128-bit IV

					// Encrypt using AES
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
								return new CipherResult<string> { ResultData = Convert.ToBase64String(msEncrypt.ToArray()) };
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				return new CipherResult<string> { ErrorText = ex.Message, ErrorCode = -999 };
			}
		}

		public CipherResult<string> GetDecryptedPassword(string value, string masterPassword)
		{
			var result = new CipherResult<string>();

			try
			{
				// Set iteration count for PBKDF2 (recommendation: at least 10000)
				int iterationCount = 10000;

				// Derive key using PBKDF2
				using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(masterPassword, new byte[16], iterationCount))
				{
					// Generate key and IV
					byte[] key = pbkdf2.GetBytes(32); // 256-bit key
					byte[] iv = pbkdf2.GetBytes(16);  // 128-bit IV

					// Decrypt using AES
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
									return new CipherResult<string> { ResultData = srDecrypt.ReadToEnd() };
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				return new CipherResult<string> { ErrorText = ex.Message, ErrorCode = -999 };
			}
		}

		public CipherResult<List<PasswordBase>> GetPasswordsByTopic(string topic)
		{
			var result = _passwordService.GetAll().Where(x => x.Topic.ToLower() == topic.ToLower()).ToList();
			return new CipherResult<List<PasswordBase>> { ResultData = result };
		}

		public CipherResult<bool> AddPassword(PasswordBase password)
		{
			_passwordService.Add(password);
			return new CipherResult<bool> { ResultData = true };
		}

		public CipherResult<bool> DeletePassword(PasswordBase password)
		{
			Predicate<PasswordBase> foundPassword = pred => pred == password;
			_passwordService.Delete(foundPassword);
			return new CipherResult<bool> { ResultData = true };
		}

		public CipherResult<bool> ChangePassword(PasswordBase password)
		{
			Predicate<PasswordBase> match = pred => pred.Created == password.Created;
			Action<PasswordBase> update = pred =>
			{
				pred.Name = password.Name;
				pred.Note = password.Note;
				pred.Password = password.Password;
				pred.PasswordEntryDesign = password.PasswordEntryDesign;
				pred.PasswordProperties = password.PasswordProperties;
				pred.PasswordScore = password.PasswordScore;
				pred.PasswordType = password.PasswordType;
				pred.StorageType = password.StorageType;
				pred.Topic = password.Topic;
				pred.Username = password.Username;
				pred.Value = password.Value;
				pred.Created = password.Created;
			};
			_passwordService.Update(match, update);
			return new CipherResult<bool> { ResultData = true };
		}
	}
}
