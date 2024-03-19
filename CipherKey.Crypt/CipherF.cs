using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CipherKey.Crypt
{
	public static class CipherF<T>
	{
		public static string Path { get; set; }
		public static string Key { get; set; }

		public static void CompressAndEncryptFolder(string folderPath, string? key = null)
		{
			string zipFilePath = System.IO.Path.Combine(Path, "temp.zip");
			ZipFile.CreateFromDirectory(folderPath, zipFilePath);

			byte[] zipFileBytes = File.ReadAllBytes(zipFilePath);
			byte[] encryptedZipBytes = Encrypt(zipFileBytes, key ?? Key);
			File.WriteAllBytes(Path + ".cipher", encryptedZipBytes);
			File.Delete(zipFilePath);
		}

		public static void DecryptAndDecompressFolder(string outputPath, string? key = null)
		{
			byte[] encryptedZipBytes = File.ReadAllBytes(Path + ".cipher");
			byte[] decryptedZipBytes = Decrypt(encryptedZipBytes, key ?? Key);
			string tempZipFilePath = System.IO.Path.Combine(Path, "temp.zip");
			File.WriteAllBytes(tempZipFilePath, decryptedZipBytes);
			ZipFile.ExtractToDirectory(tempZipFilePath, outputPath);
			File.Delete(tempZipFilePath);
		}




		public static void Save(T data)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (MemoryStream memoryStream = new MemoryStream())
			{
				serializer.Serialize(memoryStream, data);
				byte[] encryptedBytes = Encrypt(memoryStream.ToArray(), Key);
				if (!File.Exists(Path))
				{
					File.Create(Path).Close();
				}
				File.WriteAllBytes(Path, encryptedBytes);
			}
		}
		public static T Load()
		{
			if (!File.Exists(Path))
			{
				Save(default);
			}

			byte[] encryptedBytes = File.ReadAllBytes(Path);
			byte[] decryptedBytes = Decrypt(encryptedBytes, Key);

			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (MemoryStream memoryStream = new MemoryStream(decryptedBytes))
			{
				return (T)serializer.Deserialize(memoryStream);
			}
		}
		public static byte[] Encrypt(byte[] data, string key)
		{
			byte[] keyBytes = Encoding.UTF8.GetBytes(key);
			byte[] encryptedData = new byte[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				encryptedData[i] = (byte)(data[i] ^ keyBytes[i % keyBytes.Length]);
			}
			return encryptedData;
		}

		public static byte[] Decrypt(byte[] data, string key)
		{
			return Encrypt(data, key);
		}
		public static void CreateIfNotExist()
		{
			if(!File.Exists(Path))
			{
				Save(default(T));
			}
		}
	}
}
