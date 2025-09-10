using Atomic.Elements;
using UnityEngine;

public sealed class HelloWorldSceneAction : SceneActionAbstract
{
    public override void Invoke() => Debug.Log("Hello world");
}