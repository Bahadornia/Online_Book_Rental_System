using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Exceptions
{
    public sealed class InvalidIdentifierException: Exception
    {
        public InvalidIdentifierException(string name):base($"Id can not be zero or negative: {name}")
        { }
    }
}
