using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Logs a message to the console.
    /// </summary>
    /// <remarks>
    /// Uses Debug.Log in Unity, Console.WriteLine otherwise.
    /// </remarks>
    [Serializable]
    public sealed class PrintAction : IAction
    {
#if ODIN_INSPECTOR && UNITY_5_3_OR_NEWER
        [GUIColor(1f, 0.92156863f, 0.015686275f)]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
        private LogType _logType;
#endif

#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private string _message;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintAction"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.
        /// It allows the inspector to create and serialize a default instance of <see cref="PrintAction"/>.
        /// </remarks>
        public PrintAction()
        {
        }
        
        /// <summary>
        /// Creates a new <see cref="ConsoleAction"/> instance.
        /// </summary>
        /// <param name="message">The message to log.</param>
#if UNITY_5_3_OR_NEWER
        /// <param name="logType">The log type (default is <see cref="LogType.Log"/>).</param>
        public PrintAction(string message, LogType logType = LogType.Log)
        {
            _message = message;
            _logType = logType;
        }
#else
        public ConsoleAction(string message)
        {
            _message = message;
        }
#endif

        /// <summary>
        /// Logs the message.
        /// </summary>
        public void Invoke()
        {
#if UNITY_5_3_OR_NEWER
            Debug.unityLogger.Log(_logType, _message);
#else
            Console.WriteLine(_message);
#endif
        }
    }
}