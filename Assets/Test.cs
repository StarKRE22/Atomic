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
            IAction action = new BasicAction(() => Debug.Log("Hello world!"));
            BasicEvent basicEvent = new BasicEvent();
            BasicRequest request = new BasicRequest();
            BasicFunction<bool> function = new BasicFunction<bool>(() => true);
                
        }
    }
}