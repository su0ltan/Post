using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.Models
{
    public class QueryParameters
    {
        private int _pageSize = 10;
        private int _pageNumber = 1;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 1) _pageSize = 1;
                else if (value > 50) _pageSize = 50;
                else _pageSize = value;
            }
        }

        public string? SortBy { get; set; } = "CreatedAt";
        public string SortDirection { get; set; } = "asc"; 
        public string? SearchTerm { get; set; }
    }

}
