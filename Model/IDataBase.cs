using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBiBiapp.Model
{
    public interface IDataBase
    {
        public IEnumerable<User> GetAllUsers();

        public User GetUser(int itemPosition);

        public void AddUser(User user);

        public bool CheckIfUserIsCorrect(UserLogin user);

        public void DeleteUser(User user);
    }
}
