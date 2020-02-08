using System;

namespace SMWControlLibRendering.Exceptions
{
    /// <summary>
    /// The not c p u bitmap buffer exception.
    /// </summary>
    public class ArgumentNotIndexedGPUBitmapBufferException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotIndexedGPUBitmapBufferException"/> class.
        /// </summary>
        /// <param name="argumentName">The argument name.</param>
        public ArgumentNotIndexedGPUBitmapBufferException(string argumentName) : base($"Argument {argumentName} must be a GPUBitmapBuffer")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotIndexedGPUBitmapBufferException"/> class.
        /// </summary>
        /// <param name="argumentName">The argument name.</param>
        /// <param name="innerException">The inner exception.</param>
        public ArgumentNotIndexedGPUBitmapBufferException(string argumentName, Exception innerException) : base($"Argument {argumentName} must be a GPUBitmapBuffer", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentNotIndexedGPUBitmapBufferException"/> class.
        /// </summary>
        public ArgumentNotIndexedGPUBitmapBufferException()
        {
        }
    }
}
