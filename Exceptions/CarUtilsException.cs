using System;
using System.Runtime.Serialization;
using CarUtils.Extensions;

namespace CarUtils.Exceptions
{
    public class CarUtilsException : Exception
    {
        public CarUtilsException() : base()
        {
        }
        
        public CarUtilsException(string? message) : base(message)
        {
        }
        
        public CarUtilsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}