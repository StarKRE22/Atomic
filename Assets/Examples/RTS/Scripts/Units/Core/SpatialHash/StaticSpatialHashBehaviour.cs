using Atomic.Entities;

namespace RTSGame
{
    public class StaticSpatialHashBehaviour :
        IEntityInit<IUnit>,
        IEntityEnable,
        IEntityDisable
    {
        private readonly IGameContext _gameContext;
        private IUnit _self;
        private SpatialHash<IUnit> _spatialHash;

        public StaticSpatialHashBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        // Инициализация
        public void Init(IUnit entity)
        {
            _self = entity;
            _spatialHash = _gameContext.GetSpatialHash();
        }

        // Вставка в SpatialHash (один раз)
        public void Enable(IEntity entity)
        {
            _spatialHash.Insert(_self);
        }

        // Удаление из SpatialHash
        public void Disable(IEntity entity)
        {
            _spatialHash.Remove(_self);
        }
    }
}