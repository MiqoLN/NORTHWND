using System;

namespace NORTHWND.Core.Exceptions
{
    public class LogicException : Exception
    {
        public LogicException(string message):base(message)
        {

        }
    }
}
