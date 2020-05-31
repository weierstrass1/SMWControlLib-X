using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibRendering.Exceptions
{
    public class ArrayLengthNotValid : ArgumentException
    {
        public ArrayLengthNotValid(string argumentName, string message) : base($"Array {argumentName} length is not valid. {message}")
        {

        }
        public ArrayLengthNotValid(string argumentName) : base($"Array {argumentName} length is not valid.")
        {
        }
        public ArrayLengthNotValid(string argumentName, Exception innerException) : base($"Array {argumentName} length is not valid.", innerException)
        {
        }
        public ArrayLengthNotValid() : base($"Array length is not valid.")
        {
        }
    }
}
