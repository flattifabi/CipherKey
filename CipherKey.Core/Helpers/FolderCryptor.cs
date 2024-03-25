using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Core.Helpers
{
    public static class FolderCryptor
    {
		public static void EncryptFolder(string folderPath, string key)
		{
			string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

			foreach (string file in files)
			{
				EncryptFile(file, key);
			}
		}
		public static void EncryptFile(string filePath, string key)
		{
			byte[] keyBytes = new byte[16];
			Array.Copy(Encoding.UTF8.GetBytes(key), keyBytes, Math.Min(key.Length, 16)); // Der Schlüssel sollte 16, 24 oder 32 Bytes lang sein
			using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
			{
				aes.Key = keyBytes;
				aes.GenerateIV();

				using (FileStream fsInput = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
				{
					using (FileStream fsEncrypted = new FileStream(filePath + ".encrypted", FileMode.Create, FileAccess.Write))
					{
						using (ICryptoTransform encryptor = aes.CreateEncryptor())
						{
							using (CryptoStream cs = new CryptoStream(fsEncrypted, encryptor, CryptoStreamMode.Write))
							{
								fsEncrypted.Write(aes.IV, 0, aes.IV.Length);
								fsInput.CopyTo(cs);
							}
						}
					}
				}
			}

			File.Delete(filePath);
		}
		public static void DecryptFolder(string folderPath, string key)
		{
			string[] encryptedFiles = Directory.GetFiles(folderPath, "*.encrypted", SearchOption.AllDirectories);

			foreach (string encryptedFile in encryptedFiles)
			{
				DecryptFile(encryptedFile, key);
			}
		}

		public static void DecryptFile(string encryptedFilePath, string key)
		{
			byte[] keyBytes = new byte[16];
			Array.Copy(Encoding.UTF8.GetBytes(key), keyBytes, Math.Min(key.Length, 16)); // Der Schlüssel sollte 16, 24 oder 32 Bytes lang sein
			using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
			{
				aes.Key = keyBytes;

				byte[] iv = new byte[16];
				using (FileStream fsEncrypted = new FileStream(encryptedFilePath, FileMode.Open, FileAccess.Read))
				{
					fsEncrypted.Read(iv, 0, 16);

					using (FileStream fsDecrypted = new FileStream(encryptedFilePath.Replace(".encrypted", ""), FileMode.Create, FileAccess.Write))
					{
						aes.IV = iv;
						using (ICryptoTransform decryptor = aes.CreateDecryptor())
						{
							using (CryptoStream cs = new CryptoStream(fsEncrypted, decryptor, CryptoStreamMode.Read))
							{
								cs.CopyTo(fsDecrypted);
							}
						}
					}
				}
			}

			File.Delete(encryptedFilePath);
		}
	}
}
