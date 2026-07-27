using Atomic.Elements;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class MoveInputController : IUIContextInit, IUIContextTick
    {
        private const int MOUSE_CLICK_BUTTON = 0;

        private readonly IGameContext _gameContext;

        private IReactiveVariable<IGameEntity> _selectedCharacter;
        private GameBoardView _gameBoardView;
        private Camera _camera;

        private IFunction<bool> _inputCondition;
        private UICommandQueue _commandQueue;

        public MoveInputController(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IUIContext ui)
        {
            _selectedCharacter = ui.GetSelectedCharacter();
            _gameBoardView = ui.GetGameBoardView();
            _camera = ui.GetCamera();
            _inputCondition = ui.GetInputCondition();
            _commandQueue = ui.GetCommandQueue();
        }

        public void Tick(IUIContext ui, float deltaTime)
        {
            if (!Input.GetMouseButtonDown(MOUSE_CLICK_BUTTON) || !_inputCondition.Invoke())
                return;

            IGameEntity character = _selectedCharacter.Value;
            if (character == null || !_camera.RaycastTarget(Input.mousePosition, out GameBoardCellView cellView))
                return;

            Vector2Int destination = _gameBoardView.GetBoardPosition(cellView);
            this.MoveCharacter(character, destination).Forget();
        }

        private async UniTaskVoid MoveCharacter(IGameEntity character, Vector2Int destination)
        {
            if (await character.MovePlayerCharacter(destination, _gameContext))
            {
                _selectedCharacter.Value = null;
                _commandQueue.Execute().Forget(Debug.LogException);
            }
        }
    }
}