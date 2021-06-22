using System;

namespace Solid.Attributes
{
    public class PathAttribute : Attribute
    {
        public readonly string Path;

        public PathAttribute(string path)
        {
            Path = path;
        }
    }
}
