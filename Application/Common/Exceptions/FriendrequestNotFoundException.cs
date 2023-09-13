using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class FriendrequestNotFoundException : Exception
    {
        public FriendrequestNotFoundException() : base("Friendrequest not found")
        {

        }
    }
}
