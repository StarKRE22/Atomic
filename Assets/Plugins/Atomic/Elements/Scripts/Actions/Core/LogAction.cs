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
    public sealed class LogAction : IAction
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
        /// Initializes a new instance of the <see cref="LogAction"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.
        /// It allows the inspector to create and serialize a default instance of <see cref="LogAction"/>.
        /// </remarks>
        public LogAction()
        {
        }
        
        /// <summary>
        /// Creates a new <see cref="ConsoleAction"/> instance.
        /// </summary>
        /// <param name="message">The message to log.</param>
#if UNITY_5_3_OR_NEWER
        /// <param name="logType">The log type (default is <see cref="LogType.Log"/>).</param>
        public LogAction(string message, LogType logType = LogType.Log)
        {
            _message = message;
            _logType = logType;
        }
#else
        public LogAction(string message)
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

        /// <summary>
        /// Returns a message representation.
        /// </summary>
        /// <remarks>
        /// The output depends on the Unity version:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// If compiled with <c>UNITY_5_3_OR_NEWER</c>, the method returns a string in the format 
        /// "<c>{LogType}: {Message}</c>", where <c>_logType</c> is the log type and <c>_message</c> is the log text.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Otherwise, only the log message (<c>_message</c>) is returned.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <returns>
        /// A string that represents the object: either the log type with the message or just the message.
        /// </returns>
        public override string ToString()
        {
#if UNITY_5_3_OR_NEWER
        return $"{_logType}: {_message}";   
#else       
        return _message;
#endif
        }
    }
}