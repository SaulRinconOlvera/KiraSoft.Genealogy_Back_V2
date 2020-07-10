using KiraSoft.Domain.EntityBase.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace KiraSoft.Domain.Model.Identity
{
    public class User : IdentityUser<Guid>, IBaseEntity<Guid>, IBaseAuditable
    {
        public User() : base()
        { Enabled = true; }

        public User(string userName) : base(userName)
        { Enabled = true; }

        [Key]
        public override Guid Id { get; set; }

        [StringLength(128)]
        public string PersonName { get; set; }

        [StringLength(128)]
        public string Alias { get; set; }

        [StringLength(128)]
        public string Avatar { get; set; }

        [StringLength(1024)]
        public string AvatarURL { get; set; }

        [IgnoreDataMember]
        public override string PasswordHash { get; set; }

        [IgnoreDataMember]
        public override bool TwoFactorEnabled { get; set; }

        [IgnoreDataMember]
        public override bool PhoneNumberConfirmed { get; set; }

        [IgnoreDataMember]
        public override string PhoneNumber { get; set; }

        [IgnoreDataMember]
        public override string ConcurrencyStamp { get; set; }

        [IgnoreDataMember]
        public override string SecurityStamp { get; set; }

        [IgnoreDataMember]
        [EmailAddress]
        public override string NormalizedEmail { get; set; }

        [IgnoreDataMember]
        [EmailAddress]
        public override bool EmailConfirmed { get; set; }

        [IgnoreDataMember]
        public override bool LockoutEnabled { get; set; }

        [IgnoreDataMember]
        public override DateTimeOffset? LockoutEnd { get; set; }

        [IgnoreDataMember]
        public override int AccessFailedCount { get; set; }

        [IgnoreDataMember]
        public override string NormalizedUserName { get; set; }

        [IgnoreDataMember]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [IgnoreDataMember]
        [Column(TypeName = "smalldatetime")]
        public DateTime CreationDate { get; set; }

        [IgnoreDataMember]
        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        [IgnoreDataMember]
        [Column(TypeName = "smalldatetime")]
        public DateTime LastModificationDate { get; set; }

        [IgnoreDataMember]
        [StringLength(50)]
        public string DeletedBy { get; set; }

        [IgnoreDataMember]
        [Column(TypeName = "smalldatetime")]
        public DateTime? DeletionDate { get; set; }

        [IgnoreDataMember]
        public bool Enabled { get; set; }

        [NotMapped]
        public string RolesNames { get; set; }

        [NotMapped]
        public string Token { get; set; }

        [NotMapped]
        [IgnoreDataMember]
        public string ConfirmationLink { get; set; }

        public virtual ICollection<UserToken> Tokens { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public virtual ICollection<UserLogin> Logins { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
    }
}
