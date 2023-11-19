using BusinessLogic.Services;
using Core.Interfaces.Services;
using DataAccess.Data;
using DataAccess.Repository;
using TestAPI.Helpers;

namespace TestAPI.BusinessLogic.Service
{
    [TestClass]
    public class TicketService_TicketLock
    {
        static TicketDBContext _dbContext = UnitOfWorkHelpers.GetInMemoriesDBContext();
        static UnitOfWork _unitOfWork = new UnitOfWork(_dbContext,
            new ConfigurationRepository(_dbContext),
            new TicketRepository(_dbContext),
            new DiscussionRepository(_dbContext),
            new UserRepository(_dbContext));

        private readonly ITicketServices _ticketServices = new TicketService(_unitOfWork, null);

        [TestInitialize]
        public void Initialize()
        {
            UnitOfWorkHelpers.InitializeData();
            UnitOfWorkHelpers.InitializeTicketData();
        }

        [TestMethod]
        public void OPEN_TICKET_WHEN_NOT_LOCKED()
        {
            var mock = UnitOfWorkHelpers.TicketSample2();

            mock.LockedUserId = null;
            mock.LockedDate = null;
            mock.LockedUser = null;

            var result = _ticketServices.LockTicketRow(mock, 4321).Result;

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void OPEN_TICKET_WHEN_LOCKED_LESS_THAN_20_MINUTE()
        {
            var mock = UnitOfWorkHelpers.TicketSample1();

            mock.LockedUserId = 1234;
            mock.LockedDate = DateTime.Now.AddMinutes(-5);
            mock.LockedUser = new Core.Entities.User
            {
                UserId = 1234,
                Name = "Alvin"
            };

            var result = _ticketServices.LockTicketRow(mock, 4321).Result;

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Ticket is locked by Alvin", result.Message);
        }


        [TestMethod]
        public void OPEN_TICKET_WHEN_LOCKED_MORE_THAN_20_MINUTE()
        {
            var mock = UnitOfWorkHelpers.TicketSample1();

            mock.LockedUserId = 1234;
            mock.LockedDate = DateTime.Now.AddMinutes(-25);
            mock.LockedUser = new Core.Entities.User
            {
                UserId = 1234,
                Name = "Alvin"
            };

            var result = _ticketServices.LockTicketRow(mock, 4321).Result;

            Assert.IsTrue(result.IsSuccess);
        }
    }
}
