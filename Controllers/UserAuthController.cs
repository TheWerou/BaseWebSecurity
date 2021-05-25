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

        [HttpPost("RestartPassword")]
        public bool RestartPassword(UserPassRestart userPass)
        {
            if (this.jWTHandler.IsTokenValid(userPass.Token))
            {
                var ListClaims = this.jWTHandler.GetClaims(userPass.Token);
                this.jWTHandler.ConformPassReset(ListClaims[0]);
                this.db.ResetPassword(ListClaims[0], userPass);
                return true;
            }
            return false;
        }

        [HttpPost("RequestPasswordChange")]
        public void RequestPasswordChange(ResponseDTO userEmail)
        {
            var token = this.jWTHandler.GenerateToken(userEmail.Massage);
            MailSender.SendPasswordResetMail(userEmail.Massage, token);
        }

        [HttpPost("AutorizeAccount")]
        public bool AutorizeAccount(ReturnJWT token)
        {
            //https://localhost:44360/UserAuth/AutorizeAccount?token=12
            if(this.jWTHandler.IsTokenValid(token.JWTToken))
            {
                var listOfStringClaims = this.jWTHandler.GetClaims(token.JWTToken);
                this.jWTHandler.ConformLogin(listOfStringClaims[0], listOfStringClaims[1]);
                this.db.SetConformation(listOfStringClaims[0]);

                return true;
            }

            return false;
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
        public UserAdd AddUserTest(UserAdd user)
        {
            this.db.AddUser(user);

            return user;
        }

        [HttpPost("AddUser")]
        public void AddUser(UserAdd user)
        {
            this.db.AddUser(user);
            var conformToken = this.jWTHandler.GenerateTokenForAccountConform(user.Login, user.Email);
            MailSender.SendConfirmationMail(user.Email, conformToken);
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
