using System;
using System.ComponentModel;

namespace RaspberryPi.Internals.ResourceLoader.Exceptions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class MultipleResourcesFoundException : Exception
    {
        internal MultipleResourcesFoundException(string resourceFileName, string[] resourcePaths)
            : base(string.Format("Multiple resources ending with {0} found: {1}{2}", resourceFileName, Environment.NewLine, string.Join(Environment.NewLine, resourcePaths)))
        {
        }
    }
}