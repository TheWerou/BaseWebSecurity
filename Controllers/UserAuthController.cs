using Microsoft.AspNetCore.Mvc;
using OBiBiapp.JWT;
using OBiBiapp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OBiBiapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthController : ControllerBase
    {
        private IDataBase db;

        private IJWTHandler jWTHandler;

        public UserAuthController(IDataBase db, IJWTHandler jWTHandler)
        {
            this.db = db;
            this.jWTHandler = jWTHandler;
        }

        [HttpGet("xd")]
        public string Index()
        {
            return "XD";
        }

        [HttpGet("GetAllUsers")]
        public IEnumerable<User> GetAllUsers()
        {
            return this.db.GetAllUsers();
        }

        [HttpPost("AddUser")]
        public User AddUser(User user)
        {
            this.db.AddUser(user);
            return user;
        }

        [HttpPost("AddUserJWT")]
        public string AddUserJWT(User user)
        {
            this.db.AddUser(user);
            return this.jWTHandler.GenerateToken(user.Login);
        }

        [HttpPost("Login")]
        public bool LoginUser(UserLogin login )
        {
            return this.db.CheckIfUserIsCorrect(login);
        }
    }
}
