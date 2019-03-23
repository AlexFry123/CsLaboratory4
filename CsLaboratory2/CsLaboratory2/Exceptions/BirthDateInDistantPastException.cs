using System;

namespace CsLaboratory2.Exceptions
{
    internal class BirthDateInDistantPastException : Exception
    {
        internal BirthDateInDistantPastException(string message) : base(message) { }
        internal BirthDateInDistantPastException() : base("This date is in very distant past, you can't be so old!") { }
    }
}
