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

        public void AddUser(UserAdd user);

        public void ResetPassword(string email, UserPassRestart userPass);

        public void SetConformation(string login);

        public bool CheckIfUserIsCorrect(UserLogin user);

        public void DeleteUser(User user);

        public void ConformPassReset(string email);

        public void ConformLogin(string login, string email);

        public bool CheckIfAutorized(string login);

        public void AddPassRestartRequest(string email, string token);

        public void AddAutRequest(string login, string email, string token);
    }
}
