﻿using KiraSoft.Domain.EntityBase.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KiraSoft.Domain.Model.Identity
{
    public class UserLogin : IdentityUserLogin<Guid>, IBaseEntity<Guid>, IBaseAuditable
    {
        public UserLogin() : base()
        { Enabled = true; }

        //[Key]
        public Guid Id { get; set; }

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

        public virtual User User { get; set; }
    }
}
