using NNDIP.Api.Entities;

namespace NNDIP.Api.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByUsernameAndPassword(string username, string password);
        Task<User> GetByUsernameAndPasswordAsync(string username, string password);
        IEnumerable<UserRole> GetUserRoles(long userId);
        Task<IEnumerable<UserRole>> GetUserRolesAsync(long userId);
    }
}
