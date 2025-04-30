using Microsoft.AspNetCore.Identity;
namespace Post.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public virtual List<Post> Posts { get; set; }
        public virtual List<Reply> Replies { get; set; }
    }
}
