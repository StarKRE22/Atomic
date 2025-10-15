using System.Runtime.CompilerServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Provides utility methods for checking the current Unity application mode.
    /// </summary>
    public static class AtomicUtils
    {
        /// <summary>
        /// Determines whether the application is currently in Play Mode.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the application is in Play Mode; otherwise, <c>false</c>.
        /// In builds (outside the editor), always returns <c>true</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPlayMode()
        {
#if UNITY_EDITOR
            return EditorApplication.isPlaying;
#else
            return true;
#endif
        }

        /// <summary>
        /// Determines whether the application is currently in Edit Mode and not compiling.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the application is in Edit Mode and not compiling; otherwise, <c>false</c>.
        /// In builds (outside the editor), always returns <c>false</c>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEditMode()
        {
#if UNITY_EDITOR
            return !EditorApplication.isPlaying && !EditorApplication.isCompiling;
#else
            return false;
#endif
        }
    }
}