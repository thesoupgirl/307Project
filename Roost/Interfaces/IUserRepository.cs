using System.Collections.Generic;
using Roost.Models;

namespace Roost.Interfaces
{
    public interface IUserRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<UserItem> All { get; }
        UserItem Find(string id);
        void Insert(UserItem item);
        void Update(UserItem item);
        void Delete(string id);
    }
}
