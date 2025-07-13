#if UNITY_EDITOR
using System;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Events
{
    public partial class SceneEventBus
    {
        ///Events
        private static readonly IComparer<DebugEvent> _debugEventComparer = new DebugEvent.Comparer();
        private static readonly List<DebugEvent> _debugEventsCache = new();

#if ODIN_INSPECTOR
        [InlineProperty]
#endif
        private struct DebugEvent
        {
#if ODIN_INSPECTOR
            [ShowInInspector, ReadOnly]
#endif
            internal string name;

            internal readonly int id;

            public DebugEvent(string name, int id)
            {
                this.name = name;
                this.id = id;
            }
            
            public sealed class Comparer : IComparer<DebugEvent>
            {
                public int Compare(DebugEvent x, DebugEvent y)
                {
                    return string.Compare(x.name, y.name, StringComparison.Ordinal);
                }
            }
        }

#if ODIN_INSPECTOR
        [Searchable]
        [FoldoutGroup("Debug")]
        [LabelText("Events")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(Debug_UndeclareEvent),
            CustomRemoveIndexFunction = nameof(Debug_UndeclareEventAt),
            HideAddButton = true
        )]
#endif
        private List<DebugEvent> DebugEvents
        {
            get
            {
                _debugEventsCache.Clear();

                IReadOnlyCollection<int> events = _eventBus?.DeclaredEvents;
                if (events == null)
                    return _debugEventsCache;

                foreach (int key in events)
                {
                    string name = EventBusUtils.IdToName(in key);
                    _debugEventsCache.Add(new DebugEvent(name, key));
                }

                _debugEventsCache.Sort(_debugEventComparer);
                return _debugEventsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void Debug_UndeclareEvent(DebugEvent debugEvent)
        {
            if (_eventBus != null) _eventBus.Undeclare(debugEvent.id);
        }

        private void Debug_UndeclareEventAt(int index)
        {
            if (_eventBus != null) _eventBus.Undeclare(this.DebugEvents[index].id);
        }
    }
}
#endif