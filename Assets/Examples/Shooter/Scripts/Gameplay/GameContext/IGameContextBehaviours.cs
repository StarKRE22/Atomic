using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public interface IGameContextInit : IEntityInit<IGameContext>
    {
    }

    public interface IGameContextEnable : IEntityEnable<IGameContext>
    {
    }

    public interface IGameContextDisable : IEntityDisable<IGameContext>
    {
    }

    public interface IGameContextDispose : IEntityDispose<IGameContext>
    {
    }

    public interface IGameContextTick : IEntityTick<IGameContext>
    {
    }

    public interface IGameContextFixedTick : IEntityFixedTick<IGameContext>
    {
    }

    public interface IGameContextLateTick : IEntityLateTick<IGameContext>
    {
    }

    public interface IGameContextGizmos : IEntityGizmos<IGameContext>
    {
    }

}
