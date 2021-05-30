using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Base;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace OBiBiapp.Handlers.SMSHandling
{
    public class MessageSender 
    {

        public static Task SendSmsAsync(string message)
        {
            var accountSid = Environment.GetEnvironmentVariable("SMSAccountIdentification", EnvironmentVariableTarget.User);

            var authToken = Environment.GetEnvironmentVariable("SMSAccountPassword", EnvironmentVariableTarget.User);

            TwilioClient.Init(accountSid, authToken);

            return MessageResource.CreateAsync(
              to: new PhoneNumber(Environment.GetEnvironmentVariable("MyNr")),
              from: new PhoneNumber(Environment.GetEnvironmentVariable("SMSAccountFrom", EnvironmentVariableTarget.User)),
              body: message);
        }
    }
}
