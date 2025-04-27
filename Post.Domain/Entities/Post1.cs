using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Post.Domain.Shared;

namespace Post.Domain.Entities
{
    public class Post1 : BaseEntity
    {
        [Required]
        public string topic { get; set; }

        [Required]
        public string description { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public virtual List<Reply> replies { get; set; }
    }
}
