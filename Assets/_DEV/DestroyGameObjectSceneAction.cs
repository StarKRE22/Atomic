using System;
using Atomic.Elements;
using UnityEngine;

public sealed class DestroyGameObjectSceneAction : SceneActionAbstract<GameObject>
{
    public override void Invoke(GameObject arg) => Destroy(arg);
}