using System;
using UnityEngine;

namespace SampleGame.Engine
{
    [Serializable]
    public sealed class InputMap
    {
        public KeyCode forward;
        public KeyCode back;
        public KeyCode left;
        public KeyCode right;
    }
}