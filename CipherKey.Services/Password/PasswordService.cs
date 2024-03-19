using CipherKey.Core.ApplicationConstants;
using CipherKey.Core.Data;
using CipherKey.Core.Helpers;
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
		//private XmlService<PasswordBase> _passwordService;
		public PasswordService()
		{
			//_passwordService = new XmlService<PasswordBase>(FilePaths.PasswordStorageFilePath + FilePaths.PasswordsFileName);
		}
		public CipherResult<string> GetEncryptedPassword(string value, string masterPassword)
		{
			var result = new CipherResult<string>();
			try
			{
				int iterationCount = 10000;
				using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(masterPassword, new byte[16], iterationCount))
				{
					byte[] key = pbkdf2.GetBytes(32); // 256-bit key
					byte[] iv = pbkdf2.GetBytes(16);  // 128-bit IV
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

			var f = CipherF<CipherStorage>.Load();
			var passwords = f.Passwords.Where(x => x.Topic.ToLower() == topic.ToLower()).ToList();
			return new CipherResult<List<PasswordBase>> { ResultData = passwords };
		}

		public CipherResult<bool> AddPassword(PasswordBase password)
		{
			var f = CipherF<CipherStorage>.Load();
			f.Passwords.Add(password);
			CipherF<CipherStorage>.Save(f);
			return new CipherResult<bool> { ResultData = true };
		}

		public CipherResult<bool> DeletePassword(PasswordBase password)
		{
			var f = CipherF<CipherStorage>.Load();
			var foundPassword = f.Passwords.FirstOrDefault(x => x.Created == password.Created);
			if (foundPassword != null)
			{
				f.Passwords.Remove(foundPassword);
				CipherF<CipherStorage>.Save(f);
			}
			return new CipherResult<bool> { ResultData = true };
		}

		public CipherResult<bool> ChangePassword(PasswordBase password)
		{

			var t = CipherF<CipherStorage>.Load();
			var oldPassword = t.Passwords.FirstOrDefault(x => x.Created == password.Created);
			if (oldPassword != null)
			{
				t.Passwords.Remove(oldPassword);
				t.Passwords.Add(password);
				CipherF<CipherStorage>.Save(t);
			}
			return new CipherResult<bool> { ResultData = true };
		}
	}
}
