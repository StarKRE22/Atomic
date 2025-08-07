using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(fileName = "InputMap", menuName = "SampleGame/InputMap")]
    public sealed class InputMap : ScriptableObject
    {
        public KeyCode forward;
        public KeyCode back;
        public KeyCode left;
        public KeyCode right;
    }
}