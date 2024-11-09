using JobHosting.Models;

namespace jobHosting.Models.Repositories
{
    public interface IUserAccountRepository
    {
        public Task<IEnumerable<UserAccount>> GetUsersAccounts();
        public Task<UserAccount> DeleteUser(string userId);
        public Task<UserAccount> GetUser(string userId);

    }
}
