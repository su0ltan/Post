using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.DTOs.Reply
{
    public class AddReplyDto
    {
        public string replyContent { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
