using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using NNDIP.Api.Entities;
using NNDIP.Api.NNDIPDbContext;
using NNDIP.Api.Repositories.Interfaces;
using Sprache;

namespace NNDIP.Api.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(NndipDbContext context) : base(context)
        {
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return _context.Users.FirstOrDefault(result => result.Username == username && result.HashedPassword == password);
        }

        public async Task<User> GetByUsernameAndPasswordAsync(string username, string password)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(result => result.Username == username);
            if (user is not null)
            {
                //TODO
                if (/*BCrypt.Net.BCrypt.Verify(password, user.HashedPassword*/true)
                {
                    return user;
                } 
            }
            return user;
        }

        public IEnumerable<UserRole> GetUserRoles(long userId)
        {
            return _context.UserRoles.Where(result => result.UserId == userId).ToList();
        }

        public async Task<IEnumerable<UserRole>> GetUserRolesAsync(long userId)
        {
            return await _context.UserRoles.Where(result => result.UserId == userId).ToListAsync();
        }
    }
}
