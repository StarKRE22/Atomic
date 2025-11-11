using Atomic.Entities;

namespace SampleGame
{
    public interface IActorInit : IEntityInit<IActor>
    {
    }

    public interface IActorEnable : IEntityEnable<IActor>
    {
    }

    public interface IActorDisable : IEntityDisable<IActor>
    {
    }

    public interface IActorDispose : IEntityDispose<IActor>
    {
    }

    public interface IActorTick : IEntityTick<IActor>
    {
    }

    public interface IActorFixedTick : IEntityFixedTick<IActor>
    {
    }

    public interface IActorLateTick : IEntityLateTick<IActor>
    {
    }

    public interface IActorGizmos : IEntityGizmos<IActor>
    {
    }

}
