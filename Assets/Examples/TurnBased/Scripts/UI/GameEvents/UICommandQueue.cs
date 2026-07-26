using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Game.UI
{
    public delegate UniTask UICommand(); //Annonimous!
    
    public sealed class UICommandQueue
    {
        public event Action OnCompleted;
        
        public bool IsActive { get; private set; }
    
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        private readonly Queue<IUICommand> _commandQueue = new();
    
        public void Enqueue(IUICommand command)
        {
            Log($"ENQUEUE {command.GetType().Name}", Color.cyan);
            _commandQueue.Enqueue(command);
        }
    
        public async UniTask Execute()
        {
            if (IsActive)
            {
                Log("QUEUE BUSY", Color.yellow);
                return;
            }
    
            IsActive = true;
    
            while (_commandQueue.TryDequeue(out IUICommand command))
            {
                Log($"EXECUTE {command.GetType().Name}", Color.green);
                await command.Execute();
            }
    
            IsActive = false;
    
            Log("QUEUE FINISHED", Color.magenta);
            this.OnCompleted?.Invoke();
        }
    
        private static void Log(string message, Color color)
        {
            string hex = ColorUtility.ToHtmlStringRGBA(color);
            Debug.Log($"<color=#{hex}>[UICommandQueue] {message}</color>");
        }
    }
}