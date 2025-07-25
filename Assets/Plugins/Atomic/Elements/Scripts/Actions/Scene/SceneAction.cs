#if UNITY_5_3_OR_NEWER
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> that executes a sequence of <see cref="IAction"/> instances
    /// when <see cref="Invoke"/> is called &mdash; either from code or through the Odin-generated button
    /// in the Inspector (when Odin Inspector is installed).
    /// </summary>
    /// <remarks>
    /// Attach this component to any GameObject that needs to trigger one-off or reusable logic
    /// without writing a custom script.  
    /// You can configure the action list in the Inspector or via <see cref="Construct"/>.
    /// </remarks>
    [AddComponentMenu("Atomic/Elements/Scene Action")]
    public class SceneAction : SceneActionBase
    {
        /// <summary>
        /// Actions to run when this component is invoked.
        /// They are executed in the order they appear in the array.
        /// </summary>
        [SerializeReference]
        private IAction[] actions;

        /// <summary>
        /// Programmatically assigns the internal action list.
        /// </summary>
        /// <param name="actions">Actions to be stored and executed.</param>
        /// <returns>This instance, enabling fluent chaining.</returns>
        public void Construct(params IAction[] actions) => this.actions = actions;

#if ODIN_INSPECTOR
        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
#endif
        /// <summary>
        /// Executes every action in <see cref="actions"/> sequentially.
        /// </summary>
        public override void Invoke()
        {
            if (this.actions == null)
                return;

            for (int i = 0, count = this.actions.Length; i < count; i++) 
                this.actions[i]?.Invoke();
        }
    }
}
#endif