using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Post.Domain.Shared;
namespace Post.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public virtual List<Post1> Posts { get; set; }
        public virtual List<Reply> Replies { get; set; }
    }
}
