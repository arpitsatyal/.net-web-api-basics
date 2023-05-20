using webapi.Models;
using webapi.interfaces;

namespace webapi.repository
{
    public class UserRepo : IUser
    {
        private readonly AppDbContext dbContext;
        public UserRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public ICollection<User> GetAll()
        {
            return dbContext.Users.ToList();
        }

        public async Task<User> Add(AddUsers addUsers)
        {
            var user = new User()
            {
                Id = new Random().Next(),
                Email = addUsers.Email,
                Username = addUsers.Username
            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Update(int id, UpdateUsers updateUsers)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                user.Email = updateUsers.Email;
                user.Username = updateUsers.Username;
                await dbContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<bool> Remove(int id)
        {
            var user = await dbContext.Users.FindAsync(id);
            if (user != null)
            {
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}