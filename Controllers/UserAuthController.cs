using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using OBiBiapp.Handlers.MailHandling;
using OBiBiapp.JWT;
using OBiBiapp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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

        [HttpGet("SecuredService")]
        public ResponseDTO SecuredService()
        {
            ResponseDTO retrunObject = new ResponseDTO();
            if (this.Request.Headers.ContainsKey("authorization"))
            {
                var header = this.Request.Headers;
                var listofAout = StringValues.Empty;
                header.TryGetValue("authorization", out listofAout);

                var stringJWT = listofAout.ToString().Remove(0, 7);

                if (this.jWTHandler.IsTokenValid(stringJWT))
                {
                    retrunObject.Massage = "Tajna wiadomosc";
                    return retrunObject;
                }
                else
                {
                    retrunObject.Massage = "Nie jestes zalogowany";
                    return retrunObject;
                }
            }
            else
            {
                retrunObject.Massage = "Nie jestes zalogowany";
                return retrunObject;
            }
        }

        [HttpGet("GetAllUsers")]
        public IEnumerable<User> GetAllUsers()
        {
            return this.db.GetAllUsers();
        }

        [HttpPost("AddUserTest")]
        public User AddUserTest(User user)
        {
            this.db.AddUser(user);
            return user;
        }

        [HttpPost("AddUser")]
        public bool AddUser(User user)
        {
            this.db.AddUser(user);
            return true;
        }

        [HttpPost("SendMailTest")]
        public void SendMailTest(User user)
        {
            MailSender.SendConfirmationMail(user.Email, user.Login);
        }

        [HttpPost("LoginJWT")]
        public ReturnJWT LoginJWT(UserLogin login)
        {

            if (this.db.CheckIfUserIsCorrect(login))
            {
                return new ReturnJWT()
                {
                    JWTToken = this.jWTHandler.GenerateToken(login.Login)
                };
            }
            else
            {
                return null;
            }

        }

        [HttpPost("Login")]
        public bool LoginUser(UserLogin login)
        {
            return this.db.CheckIfUserIsCorrect(login);
        }
    }
}
