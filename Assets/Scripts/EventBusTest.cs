using System;
using Atomic.Events;
using SampleGame;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DefaultNamespace
{
    public sealed class EventBusTest : MonoBehaviour
    {

        [SerializeField]
        private SceneEventBus _eventBus;

        private Subscription _hello;
        
        private void Awake()
        {
            _eventBus.SubscribeAttack(this.OnAttack);
            _hello = _eventBus.SubscribeHello(this.OnHello);
            _eventBus.SubscribeSpawn(this.OnSpawned);
        }

        [Button]
        public void UnsubscribeHello()
        {
            _hello.Dispose();
        }

        [Button]
        public void InvokeAttack(GameObject target)
        {
            _eventBus.InvokeAttack(target);
        }

        private void OnDestroy()
        {
            _eventBus.Dispose();
        }

        [Button]
        public void InvokeHello()
        {
            _eventBus.InvokeHello();
        }

        private void OnSpawned(GameObject arg1, Vector3 arg2, Quaternion arg3)
        {
            
        }

        private void OnHello()
        {
            Debug.Log("HELLO!");
        }

        private void OnAttack(GameObject obj)
        {
            Debug.Log($"ATTACKED {obj.name}");
        }
    }
}