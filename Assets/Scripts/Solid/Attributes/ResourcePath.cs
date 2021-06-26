using System;

namespace Solid.Attributes
{
    public class ResourcePath : Attribute
    {
        public readonly string Path;

        public ResourcePath(string path)
        {
            Path = path;
        }
    }
}
