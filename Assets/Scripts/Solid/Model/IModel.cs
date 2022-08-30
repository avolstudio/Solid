using System;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.Model
{
    public interface IModel<out TModel>
    {
        bool isDirty { get; }
        
        event Action<TModel> ModelChanged;
    }
}