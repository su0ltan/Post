using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Post.Domain.Shared;

namespace Post.Domain.Entities
{
    public class Post : BaseEntity
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
