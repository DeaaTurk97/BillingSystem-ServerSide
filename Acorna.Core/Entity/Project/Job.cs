using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acorna.Core.Entity
{
    [Table("Jobs")]
    public class Job : BaseEntity
    {
        [Required, MaxLength(150)]
        public string JobName { get; set; }
    }
}
