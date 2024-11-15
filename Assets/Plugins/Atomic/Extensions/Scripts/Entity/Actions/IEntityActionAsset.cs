using System;
using Atomic.Entities;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "IEntityActionCreator")]
    public interface IEntityActionAsset
    {
        Action Create(IEntity entity);
    }

    public interface IEntityActionAsset<in T>
    {
        Action<T> Create(IEntity entity);
    }
    
    public interface IEntityActionAsset<in T1, in T2>
    {
        Action<T1, T2> Create(IEntity entity);
    }
    
    public interface IEntityActionAsset<in T1, in T2, in T3>
    {
        Action<T1, T2, T3> Create(IEntity entity);
    }
}