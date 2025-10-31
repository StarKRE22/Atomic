using Atomic.Entities;

namespace BeginnerGame
{
    public interface IGameContextInit : IEntityInit<GameContext>
    {
    }

    public interface IGameContextEnable : IEntityEnable<GameContext>
    {
    }

    public interface IGameContextDisable : IEntityEnable<GameContext>
    {
    }

    public interface IGameContextDispose : IEntityDispose<GameContext>
    {
    }

    public interface IGameContextTick : IEntityTick<GameContext>
    {
    }

    public interface IGameContextFixedTick : IEntityFixedTick<GameContext>
    {
    }

    public interface IGameContextLateTick : IEntityLateTick<GameContext>
    {
    }

    public interface IGameContextGizmos : IEntityGizmos<GameContext>
    {
    }
}