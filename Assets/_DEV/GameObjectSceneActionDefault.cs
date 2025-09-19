using System;
using Atomic.Elements;
using UnityEngine;

namespace _DEV
{
    public sealed class GameObjectSceneActionDefault : SceneActionDefault<GameObject>
    {
    }
    
    [Serializable]
    public sealed class DestroyGameObjectAction : IAction<GameObject>
    {
        public void Invoke(GameObject arg) => GameObject.Destroy(arg);
    }
}