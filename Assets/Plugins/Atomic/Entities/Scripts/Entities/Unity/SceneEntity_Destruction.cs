#if UNITY_5_3_OR_NEWER
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        /// <summary>
        /// Destroys the associated GameObject of the specified <see cref="IEntity"/> if it can be cast to a <see cref="SceneEntity"/>.
        /// </summary>
        /// <param name="entity">The entity whose GameObject should be destroyed.</param>
        /// <param name="t">Optional delay in seconds before destruction. Defaults to 0.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destroy(IEntity entity, float t = 0) => Destroy(Cast(entity), t);

        /// <summary>
        /// Destroys the specified <see cref="SceneEntity"/>'s GameObject after an optional delay.
        /// </summary>
        /// <param name="entity">The <see cref="SceneEntity"/> to destroy.</param>
        /// <param name="t">Optional delay in seconds before destruction. Defaults to 0.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destroy(SceneEntity entity, float t = 0)
        {
            if (entity)
            {
                entity.Uninstall();
                Destroy(entity.gameObject, t);
            }
        }
    }
}
#endif