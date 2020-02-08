using System;

namespace SMWControlLibRendering.Exceptions
{
    /// <summary>
    /// The not c p u bitmap buffer exception.
    /// </summary>
    public class ArgumentNotCPUBitmapBufferException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotCPUBitmapBufferException"/> class.
        /// </summary>
        /// <param name="argumentName">The argument name.</param>
        public ArgumentNotCPUBitmapBufferException(string argumentName) : base($"Argument {argumentName} must be a CPUBitmapBuffer")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotCPUBitmapBufferException"/> class.
        /// </summary>
        /// <param name="argumentName">The argument name.</param>
        /// <param name="innerException">The inner exception.</param>
        public ArgumentNotCPUBitmapBufferException(string argumentName, Exception innerException) : base($"Argument {argumentName} must be a CPUBitmapBuffer", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotCPUBitmapBufferException"/> class.
        /// </summary>
        public ArgumentNotCPUBitmapBufferException()
        {
        }
    }
}
