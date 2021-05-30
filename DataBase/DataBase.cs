using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OBiBiapp.Model
{
    public class DataBase : IDataBase
    {
        public List<User> listOfUsers { get; set; }

        public List<ConformReqDto> ListOfConformToken;

        public List<ConformReqDto> PasswordChangeRequestToken;

        public List<ConformReqDto> TwoFARequests;

        public DataBase()
        {
            this.listOfUsers = new List<User>();
            this.ListOfConformToken = new List<ConformReqDto>();
            this.PasswordChangeRequestToken = new List<ConformReqDto>();
            this.TwoFARequests = new List<ConformReqDto>();
            this.CreateUser();
        }

        private void CreateUser()
        {
            var newUser = new UserAdd()
            {
                Login = "Tomek",
                Password = "Tomek",
                Email = "Tomek@Tomek.Tomek"
            };
            var newUser2 = new UserAdd()
            {
                Login = "Michal",
                Password = "Michal",
                Email = "Michal@Michal.Michal"
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

        public bool CheckIfTwoFAREqExist(string login)
        {
            var request = this.TwoFARequests.Find(r => r.Login == login );
            if(request != null)
            {
                return true;
            }
            return false;

        }

        public void DeleteUser(User user)
        {
            this.listOfUsers.Remove(user);
        }

        public void AddAutRequest(string login, string email, string token)
        {

            var conformRequest = new ConformReqDto()
            {
                Login = login,
                Email = email,
                Token = token,
            };
            this.ListOfConformToken.Add(conformRequest);
        }

        public void AddPassRestartRequest(string email, string token)
        {
            var user = this.listOfUsers.Find(u => u.Email == email);
            if (user != null)
            {
                var conformRequest = new ConformReqDto()
                {
                    Login = user.Login,
                    Email = email,
                    Token = token,
                };
                this.PasswordChangeRequestToken.Add(conformRequest);
            }

        }

        public string GenereteTokenForTwoFA()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }

        public void AddTwoFARequest(UserLogin user, string token)
        {
            var user2 = this.listOfUsers.Find(u => u.Login == user.Login);
            var conformRequest = new ConformReqDto()
            {
                Login = user.Login,
                Email = user2.Email,
                Token = token,
            };
            this.TwoFARequests.Add(conformRequest);
        }

        public void ConformTwoFALogin(UserLogin user)
        {
            var requestConfirm = this.TwoFARequests.Find(c => c.Login == user.Login);
            this.ListOfConformToken.Remove(requestConfirm);
        }

        public void ConformLogin(string login, string email)
        {
            var requestConfirm = this.ListOfConformToken.Find(c => c.Login == login && c.Login == email);
            this.ListOfConformToken.Remove(requestConfirm);
        }

        public void ConformPassReset(string email)
        {
            var requestConfirm = this.ListOfConformToken.Find(c => c.Login == email);
            this.PasswordChangeRequestToken.Remove(requestConfirm);
        }

        public bool CheckIfAutorized(string login)
        {
            var user = this.listOfUsers.Find(u => u.Login == login);
            if (user != null & user.Conformed)
            {
                return true;
            }
            return false;
        }
    }
}
