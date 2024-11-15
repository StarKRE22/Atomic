using System.Collections.Generic;
using UnityEngine;

namespace Atomic.UI
{
    internal sealed class UpdateManager : MonoBehaviour
    {
        private const string OBJECT_NAME = "Atomic.UI.ViewUpdaterV2";

        private readonly List<SceneViewController> controllers = new();
        private readonly List<SceneViewController> _cache = new();


        private static bool installed;
        private static UpdateManager _instance;

        private static UpdateManager instance
        {
            get
            {
                if (_instance == null && !installed)
                {
                    GameObject go = new GameObject(OBJECT_NAME);
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<UpdateManager>();
                    installed = true;
                }

                return _instance;
            }
        }

        internal static void AddController(SceneViewController controller)
        {
            if (controller == null)
            {
                return;
            }

            var i = instance;
            if (i == null)
            {
                return;
            }

            i.controllers.Add(controller);
        }

        internal static void RemoveController(SceneViewController controller)
        {
            if (controller == null)
            {
                return;
            }
            
            var i = instance;
            if (i == null)
            {
                return;
            }

            i.controllers.Remove(controller);
        }

        #region Unity

        private void Update()
        {
            int count = this.controllers.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.controllers);

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < count; i++)
            {
                _cache[i].OnUpdate(deltaTime);
            }
        }
        
        private void FixedUpdate()
        {
            int count = this.controllers.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.controllers);

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < count; i++)
            {
                _cache[i].OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            int count = this.controllers.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.controllers);

            float deltaTime = Time.deltaTime;
            for (int i = 0; i < count; i++)
            {
                _cache[i].OnLateUpdate(deltaTime);
            }
        }

        #endregion
    }
}