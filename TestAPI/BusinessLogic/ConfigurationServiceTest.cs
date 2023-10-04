
using BusinessLogic.Services;
using Core.Constants;
using Core.Interfaces.Repository;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using TestAPI.Helpers;

namespace TestAPI.BusinessLogic
{
	[TestClass]
	public class ConfigurationServiceTest
	{

		[TestMethod]
		public  void GetValue()
		{
			ConfigurationService _configuration = new ConfigurationService(UnitOfWorkHelpers.Get());

			var test = _configuration.GetConfigurationValue(ConfigContants.FileUpload_MaxFileSize).Result;

		}
	}
}
