using Core.Interfaces.Repository;
using DataAccess.Data;
using System.Collections;

namespace DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly TicketDBContext context;
		private Hashtable repositories;

		private ConfigurationRepository configurationRepository;
		private TicketRepository ticketRepository;
		private DiscussionRepository discussionRepository;
		private UserRepository userRepository;

		public UnitOfWork(TicketDBContext context)
		{
			this.context = context;
		}

		public IConfigurationRepository ConfigurationRepository { get
			{
				if (configurationRepository == null)
				{
					configurationRepository = new ConfigurationRepository(context);
				}
				return configurationRepository; 
			}
		}

		public ITicketRepository TicketRepository
		{
			get
			{
				if (ticketRepository == null)
				{
					ticketRepository = new TicketRepository(context);
				}
				return ticketRepository;
			}
		}

        public IDiscussionRepository DiscussionRepository
        {
            get
            {
                if (discussionRepository == null)
                {
                    discussionRepository = new DiscussionRepository(context);
                }
                return discussionRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
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
