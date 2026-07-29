#if UNITY_5_3_OR_NEWER
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// ScriptableObject responsible for automatically spawning a set of
    /// <see cref="MonoEntity"/> prefabs when a specific scene is loaded.
    /// </summary>
    /// <remarks>
    /// Scene matching is performed using a regular expression.
    /// Entity spawning can occur either before or after the scene is fully loaded.
    /// </remarks>
    [CreateAssetMenu(
        fileName = "EntityBootstrapper",
        menuName = "Atomic/Entities/EntityBootstrapper"
    )]
    public class ScriptableEntityBootstrapper : ScriptableObject
    {
        /// <summary>
        /// Defines when the entity spawning should occur.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// Spawn entities before the scene is fully loaded.
            /// </summary>
            BeforeSceneLoad = 0,

            /// <summary>
            /// Spawn entities after the scene has finished loading.
            /// </summary>
            AfterSceneLoad = 1
        }

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
#endif
        [SerializeField]
        private bool isEnabled = true;
        
#if ODIN_INSPECTOR
        [EnableIf(nameof(isEnabled))]
#endif
        [Tooltip(
            "Regular expression used to determine whether the current scene should trigger this bootstrap. " +
            "If empty, the bootstrap applies to all scenes."
        )]
        [SerializeField]
        private string _sceneRegex;

#if ODIN_INSPECTOR
        [EnableIf(nameof(isEnabled))]
#endif
        [Tooltip("Defines when the bootstrap should perform entity spawning: before or after the scene load.")]
        [SerializeField]
        private Mode _mode = Mode.BeforeSceneLoad;

#if ODIN_INSPECTOR
        [EnableIf(nameof(isEnabled))]
#endif
        [Space]
        [Tooltip("List of Entity prefabs to spawn in the scene.")]
        [SerializeField]
        private MonoEntity[] _entityPrefabs;

        /// <summary>
        /// Checks whether the given scene satisfies the bootstrap’s conditions.
        /// </summary>
        /// <param name="scene">The scene to evaluate.</param>
        /// <returns>
        /// <see langword="true"/> if the regex is empty or the scene name matches it;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        protected virtual bool IsAvailable(Scene scene)
        {
            return string.IsNullOrEmpty(_sceneRegex) || Regex.IsMatch(scene.name, _sceneRegex);
        }

        /// <summary>
        /// Instantiates all configured scene entities.
        /// If required, waits for the scene to finish loading before spawning.
        /// </summary>
        /// <param name="scene">The scene in which to spawn entities.</param>
        protected virtual async void BootstrapEntities(Scene scene)
        {
            if (_mode == Mode.AfterSceneLoad)
                while (!scene.isLoaded)
                    await Task.Yield();

            for (int i = 0, count = _entityPrefabs.Length; i < count; i++)
            {
                MonoEntity prefab = _entityPrefabs[i];
                MonoEntity.Create(prefab, scene);
            }
        }

        /// <summary>
        /// Entry point called automatically by Unity before the first scene is loaded.
        /// </summary>
        /// <remarks>
        /// Finds all <see cref="ScriptableEntityBootstrapper"/> assets in the project’s Resources folder
        /// and invokes <see cref="BootstrapEntities"/> on those whose conditions
        /// are satisfied by the active scene.
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Scene activeScene = SceneManager.GetActiveScene();

            ScriptableEntityBootstrapper[] bootstraps = Resources.LoadAll<ScriptableEntityBootstrapper>(string.Empty);
            int count = bootstraps.Length;
            if (count == 0)
                return;

            for (int i = 0; i < count; i++)
            {
                ScriptableEntityBootstrapper bootstrapper = bootstraps[i];
                if (bootstrapper.isEnabled && bootstrapper.IsAvailable(activeScene))
                    bootstrapper.BootstrapEntities(activeScene);
            }
        }
    }
}
#endif