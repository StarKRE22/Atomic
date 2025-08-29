using UnityEngine;

namespace ShooterGame.Gameplay
{
    [CreateAssetMenu(
        fileName = "InputMap",
        menuName = "ShooterGame/New InputMap"
    )]
    public sealed class InputMap : ScriptableObject
    {
        [field: SerializeField]
        public KeyCode MoveLeft { get; private set; } = KeyCode.LeftArrow;

        [field: SerializeField]
        public KeyCode MoveRight { get; private set; } = KeyCode.RightArrow;

        [field: SerializeField]
        public KeyCode MoveForward { get; private set; } = KeyCode.UpArrow;

        [field: SerializeField]
        public KeyCode MoveBack { get; private set; } = KeyCode.DownArrow;

        [field: SerializeField]
        public KeyCode Fire { get; private set; } = KeyCode.Space;
    }
}