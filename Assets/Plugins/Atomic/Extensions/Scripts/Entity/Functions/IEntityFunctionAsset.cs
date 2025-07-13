using System;
using Atomic.Entities;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    public interface IEntityFunctionAsset<out R>
    {
        Func<R> Create(IEntity entity);
    }

    public interface IEntityFunctionAsset<in T, out R>
    {
        Func<T, R> Create(IEntity entity);
    }

    public interface IEntityFunctionAsset<in T1, in T2, out R>
    {
        Func<T1, T2, R> Create(IEntity entity);
    }

    public interface IEntityFunctionAsset<in T1, in T2, in T3, out R>
    {
        Func<T1, T2, T3, R> Create(IEntity entity);
    }
}