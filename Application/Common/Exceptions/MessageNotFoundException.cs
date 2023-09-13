using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class MessageNotFoundException : Exception
    {
        public MessageNotFoundException() :base("Message not found")
        {

        }
    }
}
