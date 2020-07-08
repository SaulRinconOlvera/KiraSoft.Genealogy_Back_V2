using KiraSoft.Domain.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace KiraSoft.Domain.Model.Identity
{
    public class TokenHistory : BaseAuditable<Guid>
    {
        [Required]
        public Guid TokenId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public DateTime Iat { get; set; }

        [Required]
        public DateTime Nbf { get; set; }

        [Required]
        public DateTime Exp { get; set; }

        [Required]
        public string Audience { get; set; }

        [Required]
        public string Issuer { get; set; }

        public virtual User User { get; set; }
    }
}
