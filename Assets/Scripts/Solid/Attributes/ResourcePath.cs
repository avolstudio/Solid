using System;
/*Copyright (c) Created by Oleksii Volovich 2021*/
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