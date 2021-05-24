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
            this.CreateUser();
        }

        private void CreateUser()
        {
            var newUser = new UserAdd()
            {
                Login = "Tomek",
                Password = "Tomek",
                Email = "Tomek,"
            };
            var newUser2 = new UserAdd()
            {
                Login = "Michal",
                Password = "Michal",
                Email = "Michal,"
            };
            AddUser(newUser);
            AddUser(newUser2);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return this.listOfUsers;
        }

        public void ResetPassword(string email, UserPassRestart userPass)
        {
            var objectTochange = this.listOfUsers.FindIndex(c => c.Email == email);
            using (SHA256 mySHA256 = SHA256.Create())
            {
                this.listOfUsers[objectTochange].Password = Encoding.ASCII.GetString(mySHA256.ComputeHash(Encoding.ASCII.GetBytes(userPass.Password))); 
            } 
        }

        public void SetConformation(string login)
        {
            var objectTochange = this.listOfUsers.FindIndex(c => c.Login == login);
            this.listOfUsers[objectTochange].Conformed = true;
        }

        public void AddUser(UserAdd user)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                user.Password = Encoding.ASCII.GetString(mySHA256.ComputeHash(Encoding.ASCII.GetBytes(user.Password)));
            }
            var newUser = new User()
            {
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
            };
            this.listOfUsers.Add(newUser);
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
