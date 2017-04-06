using System.Collections.Generic;
using System.Linq;
using Roost.Interfaces;
using Roost.Models;

namespace Roost.Services
{
    public class UserRepository : IUserRepository
    {
        private List<UserItem> _users;

        public UserRepository()
        {
            InitializeData();
        }

        public IEnumerable<UserItem> All
        {
            get { return _users; }
        }

        public bool DoesItemExist(string id)
        {
	    //check id if valid
	    if(id == null || id.Length <= 0)
	    {
		//invalid id
		return false;
	    }
            return _users.Any(item => item.ID == id);
        }

        public UserItem Find(string id)
        {
	    //validate id
            if(id == null || id.Length <= 0)
	    {
		//invalid input, returning null
		return null;
	    }
            return _users.FirstOrDefault(item => item.ID == id);
        }

        public void Insert(UserItem item)
        {
	    //validate item
	    if(item == null)
	    {
	    	//null item, returning
		return;
	    }
            _users.Add(item);
        }

        public void Update(UserItem item)
        {
	    //validate item
	    if(item == null)
	    {
		//null item, returning
		return;
	    }
            var userItem = this.Find(item.ID);
            var index = _users.IndexOf(item);
            _users.RemoveAt(index);
            _users.Insert(index, item);
        }

        public void Delete(string id)
        {
	    //validate id
	    if(id == null || id.Length <= 0)
	    {
		//invalid input, returning
		return;
	    }
            _users.Remove(this.Find(id));
        }

        private void InitializeData()
        {
            _users = new List<UserItem>();

            var userItem1 = new UserItem
            {
                ID = "6bb8a868-dba1-4f1a-93b7-24ebce87e243",
                Name = "Learn app development"
            };

            var userItem2 = new UserItem
            {
                ID = "b94afb54-a1cb-4313-8af3-b7511551b33b",
                Name = "Develop apps",
            };

            var userItem3 = new UserItem
            {
                ID = "ecfa6f80-3671-4911-aabe-63cc442c1ecf",
                Name = "Publish apps",
            };

            _users.Add(userItem1);
            _users.Add(userItem2);
            _users.Add(userItem3);
        }
    }
}
