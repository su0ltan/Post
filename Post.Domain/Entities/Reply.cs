using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Post.Domain.Shared;

namespace Post.Domain.Entities
{
    public class Reply : BaseEntity
    {
        public string replyContent { get; set; }

        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }

        public virtual Post Post { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
    }
}
