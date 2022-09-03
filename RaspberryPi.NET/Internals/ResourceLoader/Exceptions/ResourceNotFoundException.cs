using System;
using System.ComponentModel;

namespace RaspberryPi.Internals.ResourceLoader.Exceptions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ResourceNotFoundException : Exception
    {
        internal ResourceNotFoundException(string resourceFileName)
            : base(string.Format("Resource ending with {0} not found.", resourceFileName))
        {
        }
    }
}