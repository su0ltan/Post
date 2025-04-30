using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.Models
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
    }

}
