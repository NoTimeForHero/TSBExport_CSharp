using System;

namespace TSBExport_CSharp
{
    public class UnknownTypeException : Exception
    {
        public UnknownTypeException(string message) : base(message) {}
    }
}