using Core.Entities;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace TestAPI.Helpers
{
	public static class UnitOfWorkHelpers
	{
		public static UnitOfWork GetActualSqlServer()
		{
			DbContextOptions options = new DbContextOptionsBuilder<TicketDBContext>()
			.UseSqlServer(new SqlConnection("Server=localhost\\sqlexpress;Database=ticketdb;Trusted_Connection=True;TrustServerCertificate=true;"))
			.Options;

			TicketDBContext dBContext = new TicketDBContext(options);

			return new UnitOfWork(dBContext, null, null, null ,null);
		}

		public static TicketDBContext GetActualSqlServerDbContext()
		{
			DbContextOptions options = new DbContextOptionsBuilder<TicketDBContext>()
			.UseSqlServer(new SqlConnection("Server=localhost\\sqlexpress;Database=ticketdb;Trusted_Connection=True;TrustServerCertificate=true;"))
			.Options;

			return new TicketDBContext(options);
		}

		static bool dataalreadyInitialize = false;
		static bool ticketalreadyInitialize = false;

		public static UnitOfWork GetInMemories()
		{
            return new UnitOfWork(null, null, null, null, null);
        }

        public static TicketDBContext GetInMemoriesDBContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder<TicketDBContext>()
            .UseInMemoryDatabase(databaseName: "ticketDb")
            .Options;

            return new TicketDBContext(options);
        }

        public static void InitializeData()
		{
			if (dataalreadyInitialize) return;
			dataalreadyInitialize = true;

			DbContextOptions options = new DbContextOptionsBuilder<TicketDBContext>()
			.UseInMemoryDatabase(databaseName: "ticketDb")
			.Options;

			using (var dBContext = new TicketDBContext(options))
			{
				//Category
				dBContext.Categories.Add(new Category { CategoryId = 1, CategoryName = "Application Bug" });
				dBContext.Categories.Add(new Category { CategoryId = 2, CategoryName = "Network Issue" });
				dBContext.Categories.Add(new Category { CategoryId = 3, CategoryName = "Server Issue" });

				//Product
				dBContext.Products.Add(new Product { ProductId = 1, ProductName = "My Applications 1" }) ;
				dBContext.Products.Add(new Product { ProductId = 2, ProductName = "My Applications 2" }) ;
				dBContext.Products.Add(new Product { ProductId = 3, ProductName = "My Applications 3" });
				dBContext.Products.Add(new Product { ProductId = 4, ProductName = "My Applications 4" });

				dBContext.Priorities.Add(new Priority { PriorityId = 1, PriorityName = "Low", ExpectedLimit = 240 });
				dBContext.Priorities.Add(new Priority { PriorityId = 2, PriorityName = "Medium", ExpectedLimit = 120 });
				dBContext.Priorities.Add(new Priority { PriorityId = 3, PriorityName = "High", ExpectedLimit = 4 });

				dBContext.Statuses.Add(new Status { StatusId = 1, Name = "New", StatusGroupId = 0 });
				dBContext.Statuses.Add(new Status { StatusId = 2, Name = "Open", StatusGroupId = 1 });
				dBContext.Statuses.Add(new Status { StatusId = 3, Name = "Closed", StatusGroupId = 2 });

				dBContext.Users.Add(new User { UserId = 1, Name = "Customer", Email = "customer@gmail.com", RoleId = 1, ImagePath = "", PasswordHash = new byte[12], PasswordSalt = new byte[12] });
				dBContext.Users.Add(new User { UserId = 2, Name = "Admin", Email = "admin1@gmail.com", RoleId = 2, ImagePath = "", PasswordHash = new byte[12], PasswordSalt = new byte[12] });
				dBContext.Users.Add(new User { UserId = 3, Name = "Admin 2", Email = "admin2@gmail.com", RoleId = 2, ImagePath = "", PasswordHash = new byte[12], PasswordSalt = new byte[12] });
				dBContext.Users.Add(new User { UserId = 4, Name = "Admin 3", Email = "admin2@gmail.com", RoleId = 2, ImagePath = "", PasswordHash = new byte[12], PasswordSalt = new byte[12] });

				dBContext.SaveChanges();
			}

		}

		public static void InitializeTicketData()
		{
			if (ticketalreadyInitialize) return;
			ticketalreadyInitialize = true;

			DbContextOptions options = new DbContextOptionsBuilder<TicketDBContext>()
			.UseInMemoryDatabase(databaseName: "ticketDb")
			.Options;

			using (var dBContext = new TicketDBContext(options))
			{
				dBContext.Tickets.Add(TicketSample1());

				dBContext.Tickets.Add(TicketSample2());


				dBContext.Tickets.Add(
				new Ticket
				{
					TicketId = 3,
					Summary = "Summary number three",
					Description = "description",
					RaisedDate = new DateTime(2023, 10, 23),
					ExpectedDate = new DateTime(2023, 10, 13),
					UserId = 1,
					ProductId = 3,
					CategoryId = 3,
					PriorityId = 3,
					StatusId = 2,
					AssignedToId = 1,
                    RowVersion = new byte[12]
                });



				dBContext.Tickets.Add(
				new Ticket
				{
					TicketId = 4,
					Summary = "Summary number Four",
					Description = "description",
					RaisedDate = new DateTime(2023, 10, 23),
					ExpectedDate = new DateTime(2023, 10, 23),
					UserId = 2,
					ProductId = 4,
					CategoryId = 3,
					PriorityId = 2,
					StatusId = 1,
					AssignedToId = 2,
                    RowVersion = new byte[12]
                });

				dBContext.SaveChanges();
			}
		}

		#region FakeData

		public static Ticket TicketSample1()
		{
			return new Ticket
			{
				TicketId = 1,
				Summary = "Summary number one",
				Description = "description",
				RaisedDate = new DateTime(2023, 10, 23),
				ExpectedDate = new DateTime(2023, 10, 23),
				UserId = 1,
				ProductId = 1,
				CategoryId = 1,
				PriorityId = 1,
				StatusId = 1,
				AssignedToId = 1,
				RowVersion = new byte[12]
			};
        }

        public static Ticket TicketSample2()
        {
			return new Ticket
			{
				TicketId = 2,
				Summary = "Summary number two",
				Description = "description",
				RaisedDate = new DateTime(2023, 10, 23),
				ExpectedDate = new DateTime(2023, 10, 23),
				UserId = 1,
				ProductId = 2,
				CategoryId = 2,
				PriorityId = 2,
				StatusId = 2,
				AssignedToId = 1,
				RowVersion = new byte[12]
			};
        }
        #endregion

    }
}
