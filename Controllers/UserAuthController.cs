using Microsoft.AspNetCore.Mvc;
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
        public UserAuthController(IDataBase db)
        {
            this.db = db;
        }

        [HttpGet("xd")]
        public string Index()
        {
            return "XD";
        }

        [HttpPost("AddUser")]
        public User AddUser(User user)
        {
            this.db.AddUser(user);
            return user;
        }

        [HttpPost("Login")]
        public bool LoginUser(UserLogin login )
        {
            return this.db.CheckIfUserIsCorrect(login);
        }
    }
}
