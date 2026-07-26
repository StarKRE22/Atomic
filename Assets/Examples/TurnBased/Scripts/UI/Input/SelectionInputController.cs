using Atomic.Elements;
using Atomic.Entities;
using Game.Gameplay;
using UnityEngine;

namespace Game.UI
{
    public sealed class SelectionInputController : IUIContextInit, IUIContextTick
    {
        private const int MOUSE_CLICK_BUTTON = 0;

        private IReactiveVariable<IGameEntity> _selectedCharacter;
        private Camera _camera;
        private IFunction<bool> _inputCondition;

        public void Init(IUIContext ui)
        {
            _selectedCharacter = ui.GetValue(UIContextAPI.SelectedCharacter);
            _camera = ui.GetValue(UIContextAPI.Camera);
            _inputCondition = ui.GetValue(UIContextAPI.InputCondition);
        }

        public void Tick(IUIContext entity, float deltaTime)
        {
            if (!Input.GetMouseButtonDown(MOUSE_CLICK_BUTTON) || !_inputCondition.Invoke() ||
                !_camera.RaycastTarget(Input.mousePosition, out GameEntityView targetView))
                return;

            IGameEntity target = targetView.Entity;
            if (target.GetValue(GameEntityAPI.EntityType).Value != GameEntityType.Character)
                return;

            _selectedCharacter.Value = _selectedCharacter.Value == target ? null : target;
        }
    }
}