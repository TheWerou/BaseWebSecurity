using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OBiBiapp.Model
{
    public class DataBase: IDataBase
    {
        public List<User> listOfUsers { get; set; }

        public DataBase()
        {
            this.listOfUsers = new List<User>();
        }

        public void AddUser(User user)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                user.Password = Encoding.ASCII.GetString(mySHA256.ComputeHash(Encoding.ASCII.GetBytes(user.Password)));
            }
            this.listOfUsers.Add(user);
        }

        public User GetUser(int itemPosition)
        {
            return listOfUsers[itemPosition];
        }

        public bool CheckIfUserIsCorrect(UserLogin user)
        {
            var login = this.listOfUsers.Find(u => u.Login == user.Login);
            if (login != null)
            {
                string hashpass;
                using (SHA256 mySHA256 = SHA256.Create())
                {
                    hashpass = Encoding.ASCII.GetString(mySHA256.ComputeHash(Encoding.ASCII.GetBytes(user.Password)));
                }
                if (login.Password == hashpass)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void DeleteUser(User user)
        {
            this.listOfUsers.Remove(user);
        }

    }
}
