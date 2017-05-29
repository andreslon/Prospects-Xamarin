using Prospects.Cross.Infrastructure.Interfaces;
using Newtonsoft.Json; 
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Cross.Android.Services
{
	public class FileService : IFileService
	{
		public async Task SaveAsync<T>(string fileName, T content)
		{
			await Task.Run(() =>
			{
				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				var filePath = Path.Combine(documentsPath, fileName);

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
				string result = JsonConvert.SerializeObject(content);
				File.WriteAllText(filePath, result);
			});
		}
		public async Task<TResponse> LoadAsync<TResponse>(string fileName)
		{
			try
			{
				return await Task.Run(async () =>
				{
					if (await Exist(fileName))
					{
						var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
						var filePath = Path.Combine(documentsPath, fileName);

						string result = System.IO.File.ReadAllText(filePath);
						if (result.Equals("{}") || string.IsNullOrEmpty(result.Trim()) || result.Equals("\"\""))
						{
							return default(TResponse);
						}
						else
						{
							TResponse serializedResponse = JsonConvert.DeserializeObject<TResponse>(result);
							return serializedResponse;
						}
					}
					else
						return default(TResponse);
				});
			}
			catch (Exception ex)
			{
				return default(TResponse);
			}
		}
		public async Task<bool> Delete(string fileName)
		{

			try
			{
				return await Task.Run<bool>(() =>
				{
					var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					var filePath = Path.Combine(documentsPath, fileName);
					if (File.Exists(filePath))
					{
						File.Delete(filePath);
						return true;
					}
					return false;
				});
			}
			catch (Exception)
			{
				return false;
			}

		}
		public async Task<bool> Exist(string fileName, bool validateDate = false)
		{
			return await Task.Run<bool>(() =>
			{
				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				var filePath = Path.Combine(documentsPath, fileName);
				if (File.Exists(filePath))
				{
					if (validateDate)
					{
						var creationTime = File.GetCreationTime(filePath);
						if (creationTime.Year <= DateTime.Now.Year && creationTime.Month <= DateTime.Now.Month && creationTime.Day < DateTime.Now.Day)
							return false;
						else
							return true;

					}
					return true;

				}
				else
				{
					return false;
				}
			});
		}


		public async Task<bool> ExistAppVersion()
		{
			return await Task.Run<bool>(() =>
			{
				var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				var filePath = Path.Combine(documentsPath, "AppVersion");
				if (File.Exists(filePath))
				{
					var creationTime = File.GetCreationTime(filePath);
					if (creationTime.AddDays(5) <= DateTime.Now)
						return false;
					else
						return true;

				}
				else
				{
					return false;
				}
			});
		}

		public string SaveFile(byte[] byteArray, string fileName, string contentFolder)
		{
			var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			folder = Path.Combine(folder, contentFolder);
			if (!Directory.Exists(folder))
			{
				Directory.CreateDirectory(folder);
			}
			var filename = Path.Combine(folder, fileName);
			File.WriteAllBytes(filename, byteArray);
			return filename;
		}

	}
}
