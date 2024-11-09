using JobHosting.Models;
using Microsoft.EntityFrameworkCore;

namespace jobHosting.Models.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly JobHostingDbContext context;
        public UserAccountRepository(JobHostingDbContext context)
        {
            this.context = context;
        }

        public async Task<UserAccount> DeleteUser(string userId)
        {
            UserAccount userToDelete = await context.UserAccounts.FirstOrDefaultAsync(user => user.Id == userId);
            if (userToDelete != null)
            {
                context.UserAccounts.Remove(userToDelete);
                await context.SaveChangesAsync();
            }

            return userToDelete;
        }

        public async Task<UserAccount> GetUser(string userId)
        {
            return await context.UserAccounts.FirstOrDefaultAsync(user => user.Id == userId); ;
        }

        public async Task<IEnumerable<UserAccount>> GetUsersAccounts()
        {
            return await context.UserAccounts.ToListAsync();
        }

        
    }
}
