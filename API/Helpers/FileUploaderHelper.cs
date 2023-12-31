﻿using Core.Entities;
using System.ComponentModel;

namespace API.Helpers
{
	public class FileUploaderHelper
	{
		private readonly string _uploadPath;

		public FileUploaderHelper(string uploadPath)
        {
			_uploadPath = uploadPath;

			if (!Directory.Exists(_uploadPath))
			{
				Directory.CreateDirectory(_uploadPath);
			}
		}

		public async Task<FileUploadResult> UploadFile(IFormCollection files)
		{
			List<FileResult> fileResults = new List<FileResult>();

			foreach (var file in files.Files) {
				if (file.FileName == null || file.FileName.Length == 0)
					continue;

				FileResult result = new FileResult();
				result.Filename = file.FileName;

				string newFileName = GenerateRandomFileName(file.FileName);
				bool UploadStatus = await UploadFile(file, newFileName);

				if (UploadStatus)
				{
					result.ServerFileName = newFileName;
					result.Status = true;
				}
				else
				{
					result.Status = false;
				}

				fileResults.Add(result);

			}

			return new FileUploadResult()
			{
				FileResults = fileResults
			};
		}

        public async Task<FileUploadResult> UploadFile(List<IFormFile> files)
        {
            List<FileResult> fileResults = new List<FileResult>();

            foreach (var file in files)
            {
                if (file.FileName == null || file.FileName.Length == 0)
                    continue;

                FileResult result = new FileResult();
                result.Filename = file.FileName;
                result.FileSize = Convert.ToInt32(file.Length);

                string newFileName = GenerateRandomFileName(file.FileName);
                bool UploadStatus = await UploadFile(file, newFileName);

                if (UploadStatus)
                {
                    result.ServerFileName = newFileName;
                    result.Status = true;
                }
                else
                {
                    result.Status = false;
                }

                fileResults.Add(result);

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
		public string ServerFileName { get; set; }
        public int FileSize { get; set; }
        public bool Status { get; set; }
	}
}
