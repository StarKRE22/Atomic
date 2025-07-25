using System;
using Atomic.Elements;
using UnityEngine;

namespace DefaultNamespace
{
    public sealed class Test : MonoBehaviour
    {
        private void Awake()
        {
            // SimpleAction action = new SimpleAction(() => Debug.Log("Hello world!"));
            BasicAction action = new BasicAction(() => Debug.Log("Hello world!"));
            BasicSignal signal = new BasicSignal()
        }
    }
}