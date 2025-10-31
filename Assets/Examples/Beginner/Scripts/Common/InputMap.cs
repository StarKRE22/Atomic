using UnityEngine;

namespace BeginnerGame
{
    [CreateAssetMenu(fileName = "InputMap", menuName = "BeginnerGame/InputMap")]
    public sealed class InputMap : ScriptableObject
    {
        public KeyCode forward;
        public KeyCode back;
        public KeyCode left;
        public KeyCode right;
    }
}