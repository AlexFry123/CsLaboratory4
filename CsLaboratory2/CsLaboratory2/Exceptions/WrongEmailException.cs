using System;

namespace CsLaboratory2.Exceptions
{
    internal class WrongEmailException : Exception
    {
        internal WrongEmailException(string message, string val) : base(message) { }
        internal WrongEmailException(string val) : base("Invalid e-mail address! Correct format: nickname@smth.net") { }
    }
}
