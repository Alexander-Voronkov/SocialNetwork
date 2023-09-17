using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class FriendrequestDto
    {
        public int? FromUserId { get; set; }
        public int? ToUserId { get; set; }
    }
}
