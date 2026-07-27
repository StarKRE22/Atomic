using Atomic.Elements;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class AttackInputController : IUIContextInit, IUIContextTick
    {
        private const int MOUSE_CLICK_BUTTON = 0;

        private readonly IGameContext _gameContext;

        private Camera _camera;
        private IReactiveVariable<IGameEntity> _selectedCharacter;
        private IFunction<bool> _inputCondition;
        private UICommandQueue _commandQueue;

        public AttackInputController(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IUIContext ui)
        {
            _camera = ui.GetCamera();
            _selectedCharacter = ui.GetSelectedCharacter();
            _inputCondition = ui.GetInputCondition();
            _commandQueue = ui.GetCommandQueue();
        }

        public void Tick(IUIContext entity, float deltaTime)
        {
            if (!Input.GetMouseButtonDown(MOUSE_CLICK_BUTTON) || !_inputCondition.Invoke())
                return;

            IGameEntity character = _selectedCharacter.Value;
            if (character == null || !_camera.RaycastTarget(Input.mousePosition, out GameEntityView targetView))
                return;

            IGameEntity target = targetView.Entity;
            this.AttackCharacter(character, target).Forget();
        }

        private async UniTaskVoid AttackCharacter(IGameEntity character, IGameEntity target)
        {
            if (await character.AttackPlayerCharacter(target, _gameContext))
            {
                _selectedCharacter.Value = null;
                _commandQueue.Execute().Forget(Debug.LogException);
            }
        }
    }
}