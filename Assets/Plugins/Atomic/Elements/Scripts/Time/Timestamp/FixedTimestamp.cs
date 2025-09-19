#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a timestamp driven by Unity's <c>Time.fixedTime</c>, updated on <c>FixedUpdate</c>.
    /// </summary>
    public class FixedTimestamp : ITimestamp
    {
        /// <inheritdoc />
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public int EndTick => _endTick;

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public int RemainingTicks => this.GetRemainingTicks();

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [ShowInInspector, HideInEditorMode]
#endif
        public float RemainingTime => this.GetRemainingTime();

        private int _endTick;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedTimestamp"/> class.
        /// </summary>
        /// <param name="endTick">Optional end tick value. Default is -1 (inactive).</param>
        public FixedTimestamp(int endTick = -1) => _endTick = endTick;

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button]
#endif
        public void StartFromSeconds(float seconds)
        {
            if (seconds < 0)
                throw new ArgumentOutOfRangeException(nameof(seconds));

            _endTick = Mathf.RoundToInt((Time.fixedTime + seconds) / Time.fixedDeltaTime);
        }

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button]
#endif
        public void StartFromTicks(int ticks)
        {
            if (ticks < 0)
                throw new ArgumentOutOfRangeException(nameof(ticks));

            this.StartFromSeconds(ticks * Time.fixedDeltaTime);
        }

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Stop() => _endTick = -1;

        /// <inheritdoc />
        public float GetProgress(float duration) => 1 - this.GetRemainingTime() / duration;

        private float GetRemainingTime()
        {
            int ticks = this.GetRemainingTicks();
            return ticks != -1 ? ticks * Time.fixedDeltaTime : 0;
        }

        private int GetRemainingTicks() => _endTick > 0
            ? Math.Max(0, _endTick - Mathf.RoundToInt(Time.fixedTime / Time.fixedDeltaTime))
            : 0;

        /// <inheritdoc />
        public bool IsIdle() => _endTick == -1;

        /// <inheritdoc />
        public bool IsPlaying() => _endTick > 0 && _endTick > CurrentTick();

        private static int CurrentTick() => Mathf.RoundToInt(Time.fixedTime / Time.fixedDeltaTime);

        /// <inheritdoc />
        public bool IsExpired() => _endTick > 0 && _endTick <= CurrentTick();
    }
}
#endif