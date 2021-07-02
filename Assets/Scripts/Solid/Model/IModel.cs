using System;

/*Copyright (c) Created by Oleksii Volovich 2021*/
namespace Solid.Model
{
    public interface IModel<out TModel>
    {
        bool IsDirty { get; }
        event Action<TModel> ModelChanged;
    }
}