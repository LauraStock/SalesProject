using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesProject.Exceptions
{
    class InvalidUserInputException : Exception
    {
        public InvalidUserInputException() : base()
        { }

        public InvalidUserInputException(string message) : base(message)
        { }
    }
}
