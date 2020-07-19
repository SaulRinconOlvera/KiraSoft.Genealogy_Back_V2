using KiraSoft.CrossCutting.Mailer.Message.Base;
using Newtonsoft.Json;
using System;

namespace KiraSoft.CrossCutting.Mailer.Message
{
    public class UserRegisterMessage : MailMessage
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("confirmationLink")]
        public string ConfirmationLink { get; set; }

        public UserRegisterMessage(string from, string to, string subject) : base(from, to, subject) { }

        protected override void ValidateInformation()
        {
            if (string.IsNullOrWhiteSpace(UserName))
                throw new ArgumentNullException("UserName");

            if (string.IsNullOrWhiteSpace(ConfirmationLink))
                throw new ArgumentNullException("ConfirmationLink");
        }

        protected override void ProcessMessage()
        {
            base.ProcessMessage();
            Message = $"Gracias por suscribirte a nuestra aplicación, <b>{ UserName }</b>. Por favor haga click en el siguiente enlace para activar su cuenta <a href='{ConfirmationLink}'>Link de confirmación</a>.";
        }
    }
}
