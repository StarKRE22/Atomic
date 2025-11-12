using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace ShooterGame.Gameplay
{
    /// <summary>
    /// A Unity <see cref="MonoBehaviour"/> that can be attached to a GameObject to perform installation logic on an <see cref="IWeapon"/> during runtime or initialization.
    /// </summary>
    /// <remarks>
    /// Used to declaratively configure entities placed in a scene.
    /// In the Editor, it supports automatic refresh via <c>OnValidate</c>.
    /// </remarks>
    public abstract class WeaponInstaller : SceneEntityInstaller<IWeapon>
    {
    }
}
