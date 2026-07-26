using Atomic.Elements;
using Atomic.Entities;
using Atomic.Events;
using Game.Gameplay;
using TMPro;

namespace Game.UI
{
    public sealed class TurnEventsPresenter : IUIContextEnable, IUIContextDisable
    {
        private readonly TMP_Text _view;
        private readonly DisposableComposite _disposables = new();
        private readonly IGameContext _gameContext;
        private IGameEventBus _eventBus;

        public TurnEventsPresenter(TMP_Text view, IGameContext gameContext)
        {
            _view = view;
            _gameContext = gameContext;
        }

        public void Enable(IUIContext entity)
        {
            _eventBus = _gameContext.GetValue(GameContextAPI.EventBus);

            _eventBus.Subscribe(GameEventAPI.PlayerTurnStarted, this.OnPlayerTurnStarted).AddTo(_disposables);
            _eventBus.Subscribe(GameEventAPI.PlayerTurnEnded, this.OnPlayerTurnEnded).AddTo(_disposables); //Main Thread
            _eventBus.Subscribe(GameEventAPI.EnemyTurnEnded, this.OnEnemyTurnEnded).AddTo(_disposables);
            _eventBus.Subscribe(GameEventAPI.EntityDamaged, this.OnDamaged).AddTo(_disposables);
            _eventBus.Subscribe(GameEventAPI.EntityAttackStarted, this.OnAttackStarted).AddTo(_disposables);
            _eventBus.Subscribe(GameEventAPI.EntityAttackEnded, this.OnAttackEnded).AddTo(_disposables);
            _eventBus.Subscribe(GameEventAPI.EntityMoved, this.OnMoved).AddTo(_disposables);

            _eventBus.Subscribe(GameEventAPI.EntityPushedOut, this.OnPushedOut).AddTo(_disposables);
            _eventBus.Subscribe(GameEventAPI.EntityPushedTarget, this.OnPushedInTarget).AddTo(_disposables);
            _eventBus.Subscribe(GameEventAPI.EntityPushed, this.OnPushed).AddTo(_disposables);

            _eventBus.Subscribe(GameEventAPI.EntityDied, this.OnDied);
            _eventBus.Subscribe(GameEventAPI.EntitySpawned, this.OnSpawned);
        }
        
        public void Disable(IUIContext entity) => 
            _disposables.Dispose();

        private void OnSpawned(SpawnEventArgs args) =>
            _view.text += $"\n{args.entity.Name}({args.entity.InstanceID}) was spawned at {args.position}";

        private void OnDied(IGameEntity target) => _view.text += $"\n{target.Name}({target.InstanceID}) was died!";

        private void OnPushed(PushedEventArgs args) => _view.text +=
            $"\n{args.target.Name}({args.target.InstanceID}) was pushed to {args.startPosition + args.direction}!";

        private void OnPushedInTarget(PushTargetEventArgs pushArgs) =>
            _view.text +=
                $"\n{pushArgs.source.Name}({pushArgs.source.InstanceID}) bounds with {pushArgs.target.Name}({pushArgs.target.InstanceID})!";

        private void OnPushedOut(PushOutEventArgs args) =>
            _view.text += $"\n{args.target.Name}({args.target.InstanceID}) was pushed out!";

        private void OnMoved(MoveEventArgs args) =>
            _view.text += $"\n{args.entity.Name}({args.entity.InstanceID}) moved to {args.newPosition}";

        private void OnAttackStarted(AttackEventArgs attackArgs) =>
            _view.text +=
                $"\n{attackArgs.instigator.Name}({attackArgs.instigator.InstanceID}) start attack {attackArgs.victim.Name}({attackArgs.victim.InstanceID})";

        private void OnAttackEnded(AttackEventArgs attackArgs) =>
            _view.text +=
                $"\n{attackArgs.instigator.Name}({attackArgs.instigator.InstanceID}) completed attack {attackArgs.victim.Name}({attackArgs.victim.InstanceID})";

        private void OnPlayerTurnStarted() => _view.text += "\nPlayer turn started!";

        private void OnPlayerTurnEnded() => _view.text += "\nPlayer turn ended!";

        private void OnEnemyTurnEnded() => _view.text += "\nEnemy turn ended!";

        private void OnEnemyTurnStarted() => _view.text += "\nEnemy turn started!";

        private void OnDamaged(TakeDamageEventArgs args) =>
            _view.text += $"\n{args.victim.Name}({args.victim.InstanceID}) was damaged: {args.damage}";
    }
}