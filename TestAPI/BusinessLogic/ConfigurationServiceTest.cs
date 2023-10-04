
using AutoMapper.Execution;
using BusinessLogic.Helpers;
using BusinessLogic.Services;
using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Repository;
using Moq;
using TestAPI.Helpers;

namespace TestAPI.BusinessLogic
{
	[TestClass]
	public class ConfigurationServiceTest
	{
		[TestMethod]
		public void GET_MAX_FILE_SIZE_BUT_CONFIG_VALID()
		{
			MemoryCacheHelpers.ResetMemoryCache();

			//Define invalid config
			Configuration configuration = new Configuration() { ConfigValue = "1048576" };
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(x => x.ConfigurationRepository.GetByKeyAsync(It.IsAny<string>())).Returns(Task.FromResult(configuration));
			ConfigurationService _configuration = new ConfigurationService(mockUnitOfWork.Object);


			//Action
			var result = _configuration.GetMaxFileSizeConfiguration().Result;

			Assert.AreEqual(1048576, result);
		}

		[TestMethod]
		public void GET_MAX_FILE_SIZE_BUT_CONFIG_NOT_VALID()
		{
			MemoryCacheHelpers.ResetMemoryCache();

			//Define invalid config
			Configuration configuration = new Configuration() { ConfigValue = "invalid key" };
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(x => x.ConfigurationRepository.GetByKeyAsync(It.IsAny<string>())).Returns(Task.FromResult(configuration));
			ConfigurationService _configuration = new ConfigurationService(mockUnitOfWork.Object);


			//Action
			var result = _configuration.GetMaxFileSizeConfiguration().Result;

			Assert.AreEqual(0, result);
		}


		[TestMethod]
		public void GET_MAX_FILE_SIZE_BUT_CONFIG_NOT_EXISTS()
		{
			MemoryCacheHelpers.ResetMemoryCache();

			//Define invalid config
			var mockUnitOfWork = new Mock<IUnitOfWork>();
			mockUnitOfWork.Setup(x => x.ConfigurationRepository.GetByKeyAsync(It.IsAny<string>())).Returns(Task.FromResult<Configuration>(null));
			ConfigurationService _configuration = new ConfigurationService(mockUnitOfWork.Object);


			//Action
			var result = _configuration.GetMaxFileSizeConfiguration().Result;

			Assert.AreEqual(0, result);
		}
	}
}
