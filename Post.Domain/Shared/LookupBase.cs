using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.Shared
{
    public class LookupBase
    {
        public Guid Id { get; set; }
        public DateTime? ModifiedAT { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
}
