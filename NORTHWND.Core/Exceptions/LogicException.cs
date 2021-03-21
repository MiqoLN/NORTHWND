using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Exceptions
{
    public class LogicException : Exception
    {
        public LogicException(string message):base(message)
        {

        }
    }
}
