using Core.Entities;
using System.ComponentModel;

namespace API.Helpers
{
	public class FileUploaderHelper
	{
		private readonly string _uploadPath;

		public FileUploaderHelper(string uploadPath)
        {
			_uploadPath = uploadPath;
		}

		public async Task<FileUploadResult> UploadFile(List<IFormFile>? files)
		{
			List<FileResult> fileResults = new List<FileResult>();

			if (files != null)
			{
				foreach (var item in files)
				{
					if (item.FileName == null || item.FileName.Length == 0)
						continue;

					FileResult result = new FileResult();
					result.Filename = item.FileName;

					string newFileName = GenerateRandomFileName(item.FileName);
					bool UploadStatus = await UploadFile(item, newFileName);

					if (UploadStatus)
					{
						result.NewFileName = newFileName;
						result.Status = true;
					} else
					{
						result.Status = false;
					}

					fileResults.Add(result);
				}
			}

			return new FileUploadResult()
			{
				FileResults = fileResults
			};
		}

		public string GenerateRandomFileName(string fullFileName)
		{
			var fileName = Path.GetFileNameWithoutExtension(fullFileName);
			var ext = Path.GetExtension(fullFileName);

			var myUniqueFileName = $"{fileName}_{DateTime.Now.Ticks}{ext}";
			return myUniqueFileName;
		}

		private async Task<bool> UploadFile(IFormFile file, string fileName)
		{
			try
			{
				var path = Path.Combine(_uploadPath, fileName);

				using (FileStream stream = new FileStream(path, FileMode.Create))
				{
					await file.CopyToAsync(stream);
					stream.Close();
				}

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

    }


	public class FileUploadResult
	{
        public bool Status { 
			get 
			{
				if (FileResults.Count == 0) return false;

				//if error exist return false
				return !FileResults.Any(x => x.Status == false);
			} 
		}

		public List<FileResult> FileResults { get; set; }
    }

	public class FileResult
	{
		public string Filename { get; set; }
		public string NewFileName { get; set; }
		public bool Status { get; set; }
	}
}
