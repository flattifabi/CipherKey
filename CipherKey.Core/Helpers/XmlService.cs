using CipherKey.Core.ApplicationConstants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CipherKey.Core.Helpers
{
	public class XmlService<T>
	{
		private List<T> data;
		private string filePath;

		public XmlService(string filePath)
		{
			this.filePath = filePath;
			this.data = new List<T>();
			if (File.Exists(filePath))
				LoadDataFromXml();
			else
			{
				if(!Directory.Exists(FilePaths.PasswordStorageFilePath))
					Directory.CreateDirectory(FilePaths.PasswordStorageFilePath);
				File.Create(filePath).Close();
			}
		}
		public void Add(T item)
		{
			data.Add(item);
			SaveDataToXml();
		}
		public void Update(Predicate<T> match, Action<T> updateAction)
		{
			T item = data.Find(match);
			if(item != null)
			{
				updateAction(item);
				SaveDataToXml();
			}
		}
		public void Delete(Predicate<T> match)
		{
			T item = data.Find(match);
			if(item != null)
			{
				data.Remove(item);
				SaveDataToXml();
			}
		}
		public T Get(Predicate<T> match)
		{
			T item = data.Find(match);
			if (item != null)
				return item;
			else
				return default;
		}
		public List<T> GetAll()
		{
			LoadDataFromXml();
			return data;
		}


		private void SaveDataToXml()
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
				using(var writer = new StreamWriter(filePath))
				{
					serializer.Serialize(writer, data);
				}
			}
			catch (Exception) { }
		}

		private void LoadDataFromXml()
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
				using(var reader = new StreamReader(filePath))
				{
					data = (List<T>)serializer.Deserialize(reader);
				}
			}
			catch (Exception) { }
		}
	}

}
