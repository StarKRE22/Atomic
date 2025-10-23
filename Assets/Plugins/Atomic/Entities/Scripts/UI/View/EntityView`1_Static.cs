#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class EntityView<E>
    {
        /// <summary>
        /// Arguments used to create an <see cref="EntityView{E}"/> instance.
        /// </summary>
        [Serializable]
        public struct CreateArgs
        {
            [Tooltip("The name of the new GameObject to create for the EntityView.")]
            public string name;

            [Tooltip("Should activate and deactivate GameObject when Show / Hide are invoked?")]
            public bool controlGameObject;
            
            [Tooltip("Installers that will configure the EntityView upon creation.")]
            public List<SceneEntityInstaller> installers;
        }

        /// <summary>
        /// Creates a new <see cref="EntityView"/> GameObject and sets up its aspects.
        /// </summary>
        /// <param name="args">The creation arguments.</param>
        /// <returns>The created <see cref="EntityView"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Create<T>(in CreateArgs args = default) where T : EntityView<E>
        {
            GameObject gameObject = new GameObject(args.name);
            gameObject.SetActive(false);

            T view = gameObject.AddComponent<T>();
            
            view.installers = args.installers;
            view.controlGameObject = args.controlGameObject;

            gameObject.SetActive(true);
            return view;
        }

        /// <summary>
        /// Destroys the view and its associated GameObject after an optional delay.
        /// </summary>
        /// <param name="view">The <see cref="EntityView"/> instance to destroy.</param>
        /// <param name="time">Optional delay in seconds before destruction. Defaults to 0.</param>
        public static void Destroy(EntityView<E> view, float time = 0)
        {
            if (view)
            {
                view.Hide();
                Destroy(view.gameObject, time);
            }
        }
    }
}
#endif