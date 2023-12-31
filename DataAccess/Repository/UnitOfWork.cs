﻿using Core.Interfaces.Repository;
using DataAccess.Data;
using System.Collections;

namespace DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly TicketDBContext context;
		private Hashtable repositories;

        public IConfigurationRepository ConfigurationRepository { get; }

        public ITicketRepository TicketRepository { get; }

        public IDiscussionRepository DiscussionRepository { get; }

        public IUserRepository UserRepository { get; }

        public UnitOfWork(TicketDBContext context,
            IConfigurationRepository ConfigurationRepository,
            ITicketRepository TicketRepository,
            IDiscussionRepository DiscussionRepository,
            IUserRepository UserRepository )
		{
			this.context = context;
			this.ConfigurationRepository = ConfigurationRepository;
			this.TicketRepository = TicketRepository;
			this.DiscussionRepository = DiscussionRepository;
			this.UserRepository = UserRepository;
		}

        public async Task<int> SaveChanges()
		{
			return await context.SaveChangesAsync();
		}
		public async Task<bool> SaveChangesReturnBool()
		{
			return await context.SaveChangesAsync() > 0;
		}

		public void Dispose()
		{
			context.Dispose();
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
		{
			if (repositories == null) repositories = new Hashtable();

			var type = typeof(TEntity).Name;

			if (!repositories.ContainsKey(type))
			{
				var repositoryType = typeof(GenericRepository<>);
				var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);

				repositories.Add(type, repositoryInstance);
			}

			return (IGenericRepository<TEntity>)repositories[type];
		}
	}
}
