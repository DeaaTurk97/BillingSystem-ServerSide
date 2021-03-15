using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Acorna.Core.Entity.SystemDefinition
{
    public class Language : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public string LanguageCode { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string LanguageDirection { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string LanguageFlag { get; set; }
        public string LanguageDefaultDisply { get; set; }
    }
}
