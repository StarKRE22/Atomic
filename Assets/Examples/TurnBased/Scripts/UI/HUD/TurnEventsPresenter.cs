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
            _eventBus = _gameContext.GetEventBus();

            _eventBus.SubscribePlayerTurnStarted( this.OnPlayerTurnStarted).AddTo(_disposables);
            _eventBus.SubscribePlayerTurnEnded( this.OnPlayerTurnEnded).AddTo(_disposables); //Main Thread
            _eventBus.SubscribeEnemyTurnEnded( this.OnEnemyTurnEnded).AddTo(_disposables);
            _eventBus.SubscribeEntityDamaged( this.OnDamaged).AddTo(_disposables);
            _eventBus.SubscribeEntityAttackStarted( this.OnAttackStarted).AddTo(_disposables);
            _eventBus.SubscribeEntityAttackEnded( this.OnAttackEnded).AddTo(_disposables);
            _eventBus.SubscribeEntityMoved( this.OnMoved).AddTo(_disposables);

            _eventBus.SubscribeEntityPushedOut( this.OnPushedOut).AddTo(_disposables);
            _eventBus.SubscribeEntityPushedTarget( this.OnPushedInTarget).AddTo(_disposables);
            _eventBus.SubscribeEntityPushed( this.OnPushed).AddTo(_disposables);

            _eventBus.SubscribeEntityDied( this.OnDied);
            _eventBus.SubscribeEntitySpawned( this.OnSpawned);
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