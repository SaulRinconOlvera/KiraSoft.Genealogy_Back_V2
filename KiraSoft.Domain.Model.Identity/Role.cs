using KiraSoft.Domain.EntityBase.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KiraSoft.Domain.Model.Identity
{
    public class Role : IdentityRole<Guid>, IBaseEntity<Guid>, IBaseAuditable
    {
        public Role() : base()
        { Enabled = true; }

        public Role(string roleName) : base(roleName)
        { Enabled = true; }

        [Key]
        public override Guid Id { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreationDate { get; set; }

        [StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime LastModificationDate { get; set; }

        [StringLength(50)]
        public string DeletedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DeletionDate { get; set; }
        public bool Enabled { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }
        public virtual ICollection<RoleClaim> Claims { get; set; }
    }
}
