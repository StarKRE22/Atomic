using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atomic.Contexts
{
    public static class ContextCases
    {
        public static bool IsParent(this IContext it, IContext context)
        {
            return it.Parent == context;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IContext Install(this IContext context, IContextInstaller installer)
        {
            installer.Install(context);
            return context;
        }
        
        public static void InstallFromScene(this IContext context, Scene scene, bool includeInactive = true)
        {
            foreach (GameObject gameObject in scene.GetRootGameObjects())
            {
                SceneContextInstallerBase[] installers = gameObject
                    .GetComponentsInChildren<SceneContextInstallerBase>(includeInactive);
                foreach (var installer in installers)
                {
                    installer.Install(context);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenInit(this IContext context, Action action)
        {
            context.OnInitiazized += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenEnable(this IContext context, Action action)
        {
            context.OnEnabled += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenDisable(this IContext context, Action action)
        {
            context.OnDisabled += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenDispose(this IContext context, Action action)
        {
            context.OnDisposed += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenUpdate(this IContext context, Action<float> action)
        {
            context.OnUpdated += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenFixedUpdate(this IContext context, Action<float> action)
        {
            context.OnFixedUpdated += action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WhenLateUpdate(this IContext context, Action<float> action)
        {
            context.OnLateUpdated += action;
        }
    }
}