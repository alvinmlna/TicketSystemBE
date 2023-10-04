using API.Helpers;

namespace API.Helpers
{
	[TestClass]
	public class UnitTest1
	{
		FileUploaderHelper uploaderHelper = new FileUploaderHelper("G:\\Portofolio\\TicketManagementSystem\\BackEnd\\TicketManagement\\API\\Images\\");

		[TestMethod]
		public void GenerateFileName()
		{
			string fullFileName = "ada ada saja.png";

			var result = uploaderHelper.GenerateRandomFileName(fullFileName);
		}
	}
}