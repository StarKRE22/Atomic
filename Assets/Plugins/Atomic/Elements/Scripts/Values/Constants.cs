#if UNITY_5_3_OR_NEWER
using UnityEngine; 
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Provides a set of commonly used constant values wrapped in <see cref="Const{T}"/> for use in serialized and functional contexts.
    /// </summary>
    public static class Constants
    {
        // -------------------- Boolean --------------------

        /// <summary>
        /// A constant representing <c>true</c>.
        /// </summary>
        public static readonly Const<bool> True = true;

        /// <summary>
        /// A constant representing <c>false</c>.
        /// </summary>
        public static readonly Const<bool> False = false;

        // -------------------- Math --------------------

        /// <summary>
        /// The mathematical constant π (pi), approximately 3.14159.
        /// </summary>
        public static readonly Const<float> PI = 3.1415927f;

        /// <summary>
        /// Two times π (2π), commonly used in circular math.
        /// </summary>
        public static readonly Const<float> TwoPI = 2 * PI;

        /// <summary>
        /// Half of π (π/2), used in trigonometry.
        /// </summary>
        public static readonly Const<float> HalfPI = PI / 2f;

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Degrees to radians conversion factor (≈0.01745).
        /// </summary>
        public static readonly Const<float> Deg2Rad = Mathf.Deg2Rad;

        /// <summary>
        /// Radians to degrees conversion factor (≈57.2958).
        /// </summary>
        public static readonly Const<float> Rad2Deg = Mathf.Rad2Deg;
#endif

        /// <summary>
        /// Euler's number e, the base of the natural logarithm (≈2.71828).
        /// </summary>
        public static readonly Const<float> E = 2.7182818f;

        /// <summary>
        /// The golden ratio (≈1.61803), often used in aesthetics and layout.
        /// </summary>
        public static readonly Const<float> GoldenRatio = 1.6180339f;

        // -------------------- Time --------------------

        /// <summary>
        /// One second.
        /// </summary>
        public static readonly Const<float> Second = 1f;

        /// <summary>
        /// One minute in seconds (60).
        /// </summary>
        public static readonly Const<float> Minute = 60f;

        /// <summary>
        /// One hour in seconds (3600).
        /// </summary>
        public static readonly Const<float> Hour = 3600f;

        /// <summary>
        /// Frame time at 60 FPS (1/60 ≈ 0.01667 seconds).
        /// </summary>
        public static readonly Const<float> FrameTime60FPS = 1f / 60f;

        // -------------------- Common Values --------------------

        /// <summary>
        /// An integer zero (0).
        /// </summary>
        public static readonly Const<int> ZeroInt = 0;

        /// <summary>
        /// An integer one (1).
        /// </summary>
        public static readonly Const<int> OneInt = 1;

        /// <summary>
        /// A float zero (0.0).
        /// </summary>
        public static readonly Const<float> Zero = 0f;

        /// <summary>
        /// A float one (1.0).
        /// </summary>
        public static readonly Const<float> One = 1f;

        /// <summary>
        /// A float negative one (-1.0).
        /// </summary>
        public static readonly Const<float> NegativeOne = -1f;

        /// <summary>
        /// A float value of one half (0.5).
        /// </summary>
        public static readonly Const<float> Half = 0.5f;

        // -------------------- Physics --------------------

        /// <summary>
        /// Standard gravity on Earth (9.81 m/s²).
        /// </summary>
        public static readonly Const<float> GravityEarth = 9.81f;

        /// <summary>
        /// Default mass value, commonly used as 1 kg.
        /// </summary>
        public static readonly Const<float> DefaultMass = 1f;

        // -------------------- Vectors (Unity Specific) --------------------

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Unit vector pointing upward (0, 1, 0).
        /// </summary>
        public static readonly Const<Vector3> Up = Vector3.up;

        /// <summary>
        /// Unit vector pointing downward (0, -1, 0).
        /// </summary>
        public static readonly Const<Vector3> Down = Vector3.down;

        /// <summary>
        /// Unit vector pointing left (-1, 0, 0).
        /// </summary>
        public static readonly Const<Vector3> Left = Vector3.left;

        /// <summary>
        /// Unit vector pointing right (1, 0, 0).
        /// </summary>
        public static readonly Const<Vector3> Right = Vector3.right;

        /// <summary>
        /// Unit vector pointing forward (0, 0, 1).
        /// </summary>
        public static readonly Const<Vector3> Forward = Vector3.forward;

        /// <summary>
        /// Unit vector pointing backward (0, 0, -1).
        /// </summary>
        public static readonly Const<Vector3> Back = Vector3.back;

        /// <summary>
        /// The zero vector (0, 0, 0).
        /// </summary>
        public static readonly Const<Vector3> ZeroVector = Vector3.zero;

        /// <summary>
        /// The one vector (1, 1, 1).
        /// </summary>
        public static readonly Const<Vector3> OneVector = Vector3.one;

        // -------------------- Colors (Unity Specific) --------------------

        /// <summary>
        /// Solid white color (1, 1, 1, 1).
        /// </summary>
        public static readonly Const<Color> White = Color.white;

        /// <summary>
        /// Solid black color (0, 0, 0, 1).
        /// </summary>
        public static readonly Const<Color> Black = Color.black;

        /// <summary>
        /// Solid red color (1, 0, 0, 1).
        /// </summary>
        public static readonly Const<Color> Red = Color.red;

        /// <summary>
        /// Solid green color (0, 1, 0, 1).
        /// </summary>
        public static readonly Const<Color> Green = Color.green;

        /// <summary>
        /// Solid blue color (0, 0, 1, 1).
        /// </summary>
        public static readonly Const<Color> Blue = Color.blue;

        /// <summary>
        /// Fully transparent color (0, 0, 0, 0).
        /// </summary>
        public static readonly Const<Color> Transparent = Color.clear;
#endif
    }
}