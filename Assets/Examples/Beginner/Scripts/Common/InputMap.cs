using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Defines a simple keyboard input mapping for player movement.
    /// </summary>
    /// <remarks>
    /// This asset allows configuring custom key bindings for movement 
    /// directions such as forward, backward, left, and right.  
    /// Use <see cref="CreateAssetMenuAttribute"/> to create instances 
    /// via the Unity Editor:  
    /// <b>Assets → Create → BeginnerGame → InputMap</b>.
    /// </remarks>
    [CreateAssetMenu(fileName = "InputMap", menuName = "BeginnerGame/InputMap")]
    public sealed class InputMap : ScriptableObject
    {
        public KeyCode forward;
        public KeyCode back;
        public KeyCode left;
        public KeyCode right;
    }
}