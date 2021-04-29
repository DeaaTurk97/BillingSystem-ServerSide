using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Acorna.Core.Entity.SystemDefinition;
using Acorna.Core.Entity.Project.BillingSystem;

namespace Acorna.Core.Entity.Security
{
    [Table("Users")]
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(10)")]
        public string Gender { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "tinyint")]
        public int City { get; set; }
        [Column(TypeName = "tinyint")]
        public int Country { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string PhotoURL { get; set; }
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public virtual Language Language { get; set; }
        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
