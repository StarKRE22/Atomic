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
    /// <see cref="SceneEntity"/> prefabs when a specific scene is loaded.
    /// </summary>
    /// <remarks>
    /// Scene matching is performed using a regular expression.
    /// Entity spawning can occur either before or after the scene is fully loaded.
    /// </remarks>
    [CreateAssetMenu(
        fileName = "SceneEntityBootstrap",
        menuName = "Atomic/Entities/SceneEntityBootstrap"
    )]
    public class SceneEntityBootstrap : ScriptableObject
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
        // [GUIColor(0f, 0.83f, 1f)]
#endif
        [Tooltip(
            "Regular expression used to determine whether the current scene should trigger this bootstrap. " +
            "If empty, the bootstrap applies to all scenes."
        )]
        [SerializeField]
        private string _sceneRegex;

#if ODIN_INSPECTOR
        // [GUIColor(1f, 0.92f, 0.02f)]
#endif
        [Tooltip("Defines when the bootstrap should perform entity spawning: before or after the scene load.")]
        [SerializeField]
        private Mode _mode = Mode.BeforeSceneLoad;

        [Space]
        [Tooltip("List of SceneEntity prefabs to spawn in the scene.")]
        [SerializeField]
        private SceneEntity[] _entities;

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
        protected virtual async void SpawnEntities(Scene scene)
        {
            if (_mode == Mode.AfterSceneLoad)
                while (!scene.isLoaded)
                    await Task.Yield();

            for (int i = 0, count = _entities.Length; i < count; i++)
            {
                SceneEntity prefab = _entities[i];
                Object sceneEntity = Instantiate(prefab, scene);
                sceneEntity.name = prefab.name;
            }
        }

        /// <summary>
        /// Entry point called automatically by Unity before the first scene is loaded.
        /// </summary>
        /// <remarks>
        /// Finds all <see cref="SceneEntityBootstrap"/> assets in the project’s Resources folder
        /// and invokes <see cref="SpawnEntities(Scene)"/> on those whose conditions
        /// are satisfied by the active scene.
        /// </remarks>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            Scene activeScene = SceneManager.GetActiveScene();

            SceneEntityBootstrap[] bootstraps = Resources.LoadAll<SceneEntityBootstrap>(string.Empty);
            int count = bootstraps.Length;
            if (count == 0)
                return;

            for (int i = 0; i < count; i++)
            {
                SceneEntityBootstrap bootstrap = bootstraps[i];
                if (bootstrap.IsAvailable(activeScene))
                    bootstrap.SpawnEntities(activeScene);
            }
        }
    }
}
#endif