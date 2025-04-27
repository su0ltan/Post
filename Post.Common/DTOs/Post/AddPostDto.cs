using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.DTOs.Post
{
    public class AddPostDto
    {
        public string topic { get; set; }
        public string description { get; set; }
        public Guid UserId { get; set; }
    }
}
