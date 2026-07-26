using UnityEngine;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
#endif

namespace AllIn13DShader
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private KeyCode targetKey = KeyCode.A;
        [SerializeField] private KeyCode altKey = KeyCode.None;

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        private Key targetNewInputKey;
        private Key altNewInputKey;
        
        private void Awake()
        {
            targetNewInputKey = ConvertKeyCodeToKey(targetKey);
            altNewInputKey = ConvertKeyCodeToKey(altKey);
        }
#endif

        public bool IsKeyPressed()
        {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            if(altKey == KeyCode.None)
            {
                return Keyboard.current != null && Keyboard.current[targetNewInputKey].wasPressedThisFrame;
            }
            return Keyboard.current != null && (Keyboard.current[targetNewInputKey].wasPressedThisFrame || 
                                              Keyboard.current[altNewInputKey].wasPressedThisFrame);
#else
            if(altKey == KeyCode.None)
            {
                return Input.GetKeyDown(targetKey);
            }
            return Input.GetKeyDown(targetKey) || Input.GetKeyDown(altKey);
#endif
        }

        public KeyCode GetTargetKey() => targetKey;
        public KeyCode GetAltKey() => altKey;

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
        private Key ConvertKeyCodeToKey(KeyCode keyCode)
        {
            switch(keyCode)
            {
                case KeyCode.None: return Key.None;
                case KeyCode.Return: return Key.Enter;
                case KeyCode.KeypadEnter: return Key.NumpadEnter;
            }
            
            if(System.Enum.TryParse(keyCode.ToString(), true, out Key key))
            {
                return key;
            }

            Debug.LogWarning($"Failed to convert KeyCode {keyCode} to Key. Using default Key.None.");
            return Key.None;
        }
#endif
    }
}