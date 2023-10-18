using API.Helpers;
using Core.Constants;
using Core.Entities;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

		[TestMethod]
		public void TestToken()
		{

			var check = Enum.GetName(typeof(RoleEnum), 1);

			var stream = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWx2aW5AZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiMCIsImV4cCI6MTY5NzcyNTQwMn0.DV9Hv_c0sfhCvkrtgKVRVNrzNHGEeBa19WRx9OV7qGQ";
			var handler = new JwtSecurityTokenHandler();
			var jsonToken = handler.ReadToken(stream);
			var tokenS = jsonToken as JwtSecurityToken;

			var email = tokenS.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;
			var role = tokenS.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
		}
	}
}