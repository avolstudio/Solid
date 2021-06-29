using System;

namespace Solid.Attributes
{
    public class SceneName : Attribute
    {
        public readonly string Name;
    
        public SceneName(string name)
        {
            Name = name;
        }
    }
}
