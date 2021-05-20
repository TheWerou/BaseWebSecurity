using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OBiBiapp.Model
{
    public interface IDataBase
    {
        public void AddUser(User user);

        public User GetUser(int itemPosition);

        public bool CheckIfUserIsCorrect(UserLogin user);

        public void DeleteUser(User user);
    }
}
