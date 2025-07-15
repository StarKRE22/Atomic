#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// An action that logs a message to the Unity console using a specified <see cref="LogType"/>.
    /// </summary>
    [Serializable]
    public class LogAction : IAction
    {
#if ODIN_INSPECTOR
        [GUIColor(1f, 0.92156863f, 0.015686275f)]
#endif
        [SerializeField]
        private string _message;

        /// <summary>
        /// The log type used when logging the message (e.g., Log, Warning, Error).
        /// </summary>
        [SerializeField]
        private LogType _logType;

        /// <summary>
        /// Creates a new <see cref="LogAction"/> instance with the specified message and log type.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="logType">The log type (default is <see cref="LogType.Log"/>).</param>
        public LogAction(string message, LogType logType = LogType.Log)
        {
            _message = message;
            _logType = logType;
        }

        /// <summary>
        /// Logs the configured message to the Unity console using the specified log type.
        /// </summary>
        public void Invoke() => Debug.unityLogger.Log(_logType, _message);
    }
}
#endif