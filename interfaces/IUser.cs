using webapi.Models;
namespace webapi.interfaces
{
    public interface IUser
    {
        public ICollection<User> GetAll();
        Task<User> Add(AddUsers addUsers);

        Task<User?> Update(int id, UpdateUsers updateUsers);

        Task<bool> Remove(int id);
    }
}