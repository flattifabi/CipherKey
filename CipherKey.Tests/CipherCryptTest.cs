using CipherKey.Crypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherKey.Tests
{
	[TestFixture]
	public class CipherCryptTest
	{
		private const string TestFolderPath = "TestFolder";
		private const string TestKey = "TestKey";
		private const string CipherFilePath = "TestFile.cipher";

		[SetUp]
		public void Setup()
		{
			if (Directory.Exists(TestFolderPath))
				Directory.Delete(TestFolderPath, true);

			Directory.CreateDirectory(TestFolderPath);

			File.WriteAllText(Path.Combine(TestFolderPath, "test.txt"), "Hello, World!");
		}

		[TearDown]
		public void TearDown()
		{
			if (File.Exists(CipherFilePath))
				File.Delete(CipherFilePath);

			if (Directory.Exists(TestFolderPath))
				Directory.Delete(TestFolderPath, true);
		}

		[Test]
		public void CompressAndEncryptFolder_Success()
		{
			CipherF<string>.Path = TestFolderPath;
			CipherF<string>.Key = TestKey;

			CipherF<string>.CompressAndEncryptFolder(TestFolderPath);

			Assert.IsTrue(File.Exists(CipherFilePath));
		}

		[Test]
		public void DecryptAndDecompressFolder_Success()
		{
			CipherF<string>.Path = TestFolderPath;
			CipherF<string>.Key = TestKey;

			CipherF<string>.CompressAndEncryptFolder(TestFolderPath);

			string outputPath = "DecryptedFolder";
			CipherF<string>.DecryptAndDecompressFolder(outputPath);

			Assert.IsTrue(Directory.Exists(outputPath));
			Assert.IsTrue(File.Exists(Path.Combine(outputPath, "test.txt")));
			Assert.AreEqual("Hello, World!", File.ReadAllText(Path.Combine(outputPath, "test.txt")));
		}

		[Test]
		public void SaveAndLoad_Success()
		{
			CipherF<string>.Path = "TestFile.xml";
			CipherF<string>.Key = TestKey;

			string testData = "Test Data";
			CipherF<string>.Save(testData);

			string loadedData = CipherF<string>.Load();

			Assert.AreEqual(testData, loadedData);
		}

		[Test]
		public void CreateIfNotExist_Success()
		{
			CipherF<string>.Path = "TestFile.xml";
			CipherF<string>.Key = TestKey;

			CipherF<string>.CreateIfNotExist();

			Assert.IsTrue(File.Exists("TestFile.xml"));
		}

		[Test]
		public void EncryptAndDecrypt_Success()
		{
			string testData = "Test Data";
			string encryptedData = Encoding.UTF8.GetString(CipherF<string>.Encrypt(Encoding.UTF8.GetBytes(testData), TestKey));
			string decryptedData = Encoding.UTF8.GetString(CipherF<string>.Decrypt(Encoding.UTF8.GetBytes(encryptedData), TestKey));

			Assert.AreEqual(testData, decryptedData);
		}
	}
}
