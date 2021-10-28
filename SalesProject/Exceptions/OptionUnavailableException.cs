using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProject.Exceptions
{
    class OptionUnavailableException : Exception
    {
        public OptionUnavailableException() : base()
        { }

        public OptionUnavailableException(string message) : base(message)
        { }
    }
}
