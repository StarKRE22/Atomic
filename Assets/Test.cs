using System;
using Atomic.Elements;
using UnityEngine;

namespace DefaultNamespace
{
    public sealed class Test : MonoBehaviour
    {
        private void Awake()
        {
            IAction action = new InlineAction(() => Debug.Log("Hello world!"));
            IEvent basicEvent = new BaseEvent();
            IRequest request = new BaseRequest();
            IFunction<bool> function = new InlineFunction<bool>(() => true);

            IVariable<int> variable = new BaseVariable<int>(); 
            GameObject gameObject = new GameObject();
        }
    }
}