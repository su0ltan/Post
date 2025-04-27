using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.DTOs.Reply
{
    public class ReplyDto
    {
        public Guid Id { get; set; }
        public string replyContent { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
