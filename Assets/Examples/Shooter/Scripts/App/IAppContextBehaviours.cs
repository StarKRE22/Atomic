using Atomic.Entities;

/**
 * Created by Entity Domain Generator.
 */

namespace ShooterGame.App
{
    /// <summary>
    /// Provides initialization logic for the strongly-typed <see cref="IAppContext"/> entity.
    /// </summary>
    /// <remarks>
    /// Automatically invoked when an <see cref="IAppContext"/> instance is created and enters the initialization phase.
    /// Typically used to set up component references, register event listeners, or assign default values.
    /// </remarks>
    public interface IAppContextInit : IEntityInit<IAppContext>
    {
    }
    /// <summary>
    /// Handles enable-time logic for the strongly-typed <see cref="IAppContext"/> entity.
    /// </summary>
    /// <remarks>
    /// Automatically invoked when an <see cref="IAppContext"/> instance becomes active or enabled.
    /// Commonly used to re-enable systems or resume behavior execution.
    /// </remarks>
    public interface IAppContextEnable : IEntityEnable<IAppContext>
    {
    }
    /// <summary>
    /// Handles disable-time logic for the strongly-typed <see cref="IAppContext"/> entity.
    /// </summary>
    /// <remarks>
    /// Automatically invoked when an <see cref="IAppContext"/> instance becomes inactive or disabled.
    /// Useful for pausing updates or temporarily suspending logic without disposing the entity.
    /// </remarks>
    public interface IAppContextDisable : IEntityDisable<IAppContext>
    {
    }
    /// <summary>
    /// Provides cleanup logic for the strongly-typed <see cref="IAppContext"/> entity.
    /// </summary>
    /// <remarks>
    /// Automatically called when an <see cref="IAppContext"/> instance is destroyed or disposed.
    /// Used to release resources, unsubscribe from events, or reset state.
    /// </remarks>
    public interface IAppContextDispose : IEntityDispose<IAppContext>
    {
    }
    /// <summary>
    /// Handles per-frame update logic for the strongly-typed <see cref="IAppContext"/> entity.
    /// </summary>
    /// <remarks>
    /// Automatically invoked during the main update loop.
    /// Typically used for time-dependent gameplay logic such as movement, state updates, or input processing.
    /// </remarks>
    public interface IAppContextTick : IEntityTick<IAppContext>
    {
    }
    /// <summary>
    /// Handles fixed update logic for the strongly-typed <see cref="IAppContext"/> entity.
    /// </summary>
    /// <remarks>
    /// Automatically invoked during Unity’s fixed update cycle, synchronized with the physics system.
    /// Commonly used for deterministic or physics-based updates.
    /// </remarks>
    public interface IAppContextFixedTick : IEntityFixedTick<IAppContext>
    {
    }
    /// <summary>
    /// Handles late update logic for the strongly-typed <see cref="IAppContext"/> entity.
    /// </summary>
    /// <remarks>
    /// Automatically invoked after all standard update calls within the frame.
    /// Typically used for camera adjustments, cleanup, or visual synchronization logic.
    /// </remarks>
    public interface IAppContextLateTick : IEntityLateTick<IAppContext>
    {
    }
    /// <summary>
    /// Provides editor visualization logic for the strongly-typed <see cref="IAppContext"/> entity.
    /// </summary>
    /// <remarks>
    /// Automatically invoked when the entity is visible in the Unity Editor Scene view.
    /// Commonly used to draw debug information, wireframes, or gizmo markers.
    /// </remarks>
    public interface IAppContextGizmos : IEntityGizmos<IAppContext>
    {
    }
}
