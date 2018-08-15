using System;

namespace TSBExport_CSharp.Other
{
    public class UnknownTypeException : Exception
    {
        public UnknownTypeException(string message) : base(message) {}
    }
}