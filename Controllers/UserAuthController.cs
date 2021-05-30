using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using OBiBiapp.Handlers.MailHandling;
using OBiBiapp.Handlers.PasswordHandling;
using OBiBiapp.Handlers.SMSHandling;
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
        [HttpPost("TestPassChecker")]
        public bool TestPassChecker(string pass)
        {
            return PasswordChecker.ValidatePassword(pass);
        }

        [HttpPost("TestSMSMessage")]
        public void SendTestMessage()
        {
            MessageSender.SendSmsAsync("Wiadomosc Testowa");
        }

        [HttpPost("RestartPassword")]
        public bool RestartPassword(UserPassRestart userPass)
        {
            if (this.jWTHandler.IsTokenValid(userPass.Token))
            {
                if (PasswordChecker.ValidatePassword(userPass.Password))
                {
                    var ListClaims = this.jWTHandler.GetClaims(userPass.Token);
                    this.db.ConformPassReset(ListClaims[0]);
                    this.db.ResetPassword(ListClaims[0], userPass);
                    return true;
                }

                return false;
            }
            return false;
        }

        [HttpPost("RequestPasswordChange")]
        public void RequestPasswordChange(ResponseDTO userEmail)
        {
            var token = this.jWTHandler.GenerateToken(userEmail.Massage);
            this.db.AddPassRestartRequest(userEmail.Massage, token);
            MailSender.SendPasswordResetMail(userEmail.Massage, token);
        }

        [HttpPost("AutorizeAccount")]
        public bool AutorizeAccount(ReturnJWT token)
        {
            //https://localhost:44360/UserAuth/AutorizeAccount?token=12
            if (this.jWTHandler.IsTokenValid(token.JWTToken))
            {
                var listOfStringClaims = this.jWTHandler.GetClaims(token.JWTToken);
                this.db.ConformLogin(listOfStringClaims[0], listOfStringClaims[1]);
                this.db.SetConformation(listOfStringClaims[0]);

                return true;
            }

            return false;
        }

        [HttpGet("SecuredService")]
        public IEnumerable<User> SecuredService()
        {
            if (this.Request.Headers.ContainsKey("authorization"))
            {
                var header = this.Request.Headers;
                var listofAout = StringValues.Empty;
                header.TryGetValue("authorization", out listofAout);

                var stringJWT = listofAout.ToString().Remove(0, 7);

                if (this.jWTHandler.IsTokenValid(stringJWT))
                {

                    return this.db.GetAllUsers();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
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
            if (PasswordChecker.ValidatePassword(user.Password))
            {
                this.db.AddUser(user);
                var conformToken = this.jWTHandler.GenerateTokenForAccountConform(user.Login, user.Email);
                this.db.AddAutRequest(user.Login, user.Email, conformToken);
                MailSender.SendConfirmationMail(user.Email, conformToken);
            }

        }

        [HttpPost("SendMailTest")]
        public void SendMailTest(User user)
        {
            MailSender.SendConfirmationMail(user.Email, user.Login);
        }

        [HttpPost("RequestLogin")]
        public bool RequestLogin(UserLogin login)
        {
            if (this.db.CheckIfUserIsCorrect(login))
            {
                if (this.db.CheckIfAutorized(login.Login))
                {
                    var token = this.db.GenereteTokenForTwoFA();
                    this.db.AddTwoFARequest(login, token);
                    MessageSender.SendSmsAsync(token);
                    return true;
                }

                return false;
            }
            return false;
        }

        [HttpPost("LoginJWT")]
        public ReturnJWT LoginJWT(UserLoginWithToken login)
        {
            var loginObject = new UserLogin()
            {
                Login = login.Login,
                Password = login.Password,
            };

            if (this.db.CheckIfUserIsCorrect(loginObject))
            {
                if (this.db.CheckIfAutorized(login.Login))
                {
                    if(this.db.CheckIfTwoFAREqExist(login.Login))
                    {
                        this.db.ConformTwoFALogin(loginObject);
                        return new ReturnJWT()
                        {
                            JWTToken = this.jWTHandler.GenerateToken(login.Login)
                        };
                    }
                    
                }
                return null;
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
