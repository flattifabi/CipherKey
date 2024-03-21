using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		public static void CreateIfNotExists(string path, string key, T data)
		{
			if (!File.Exists(path))
				SaveRemote(path, key, data);
		}
		public static bool IsRemoteSourceValid(string address)
		{
			if(File.Exists(address))
			{
				if(address.EndsWith(".cipher"))
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Load the file from the remote address and decrypt it using the key (Network drive)
		/// </summary>
		/// <param name="address"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T LoadRemote(string address, string key)
		{
			byte[] encryptedBytes = File.ReadAllBytes(address);
			byte[] decryptedBytes = Decrypt(encryptedBytes, key);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream memoryStream = new MemoryStream(decryptedBytes))
            {
                return (T)serializer.Deserialize(memoryStream);
            }
            //using (MemoryStream memoryStream = new MemoryStream(decryptedBytes))
            //{
            //	return (T)serializer.Deserialize(memoryStream);
            //}
        }
		/// <summary>
		/// Save the file to the remote address and encrypt it using the key (Network drive)
		/// </summary>
		/// <param name="address"></param>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public static bool SaveRemote(string address, string key, T data)
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				using (MemoryStream memoryStream = new MemoryStream())
				{
					serializer.Serialize(memoryStream, data);
					byte[] encryptedBytes = Encrypt(memoryStream.ToArray(), key);
					if (!File.Exists(address))
					{
						File.Create(address).Close();
					}
					File.WriteAllBytes(address, encryptedBytes);
					//File.WriteAllBytes(address, memoryStream.ToArray());
				}
				return true;
			}
			catch(Exception e)
			{
				return false;
			}
		}
	}
}
