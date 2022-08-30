using System;

/*Copyright (c) Created by Oleksii Volovich 2021*/
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