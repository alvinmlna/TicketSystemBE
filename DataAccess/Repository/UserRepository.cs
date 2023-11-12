using Core.Entities;
using Core.Interfaces.Repository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(TicketDBContext ticketContext) : base(ticketContext)
        {
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.UserId == id && x.IsRemoved == false);
            return user;
        }

        public async Task<IReadOnlyList<User>> ListAllUsers(string search = "")
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
            } 
                else
            {
                search = string.Empty;
            }

            var query = await  dbContext.Set<User>()
                .Where(x => x.IsRemoved == false && x.Email.ToLower().Contains(search)).ToListAsync();

            return query;
        }
    }
}
