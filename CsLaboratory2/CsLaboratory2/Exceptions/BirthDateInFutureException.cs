using System;

namespace CsLaboratory2.Exceptions
{
    internal class BirthDateInFutureException : Exception
    {
        internal BirthDateInFutureException(string message) : base(message){ }
        internal BirthDateInFutureException() : base("This date is in Future!") { }
    }
}
