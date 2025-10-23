#if UNITY_EDITOR
using System;
using System.Collections.Generic;
// ReSharper disable NotAccessedField.Local
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

#if ODIN_INSPECTOR
            [ShowInInspector, ReadOnly]
#endif
            internal int subscriptions;

            internal readonly int id;

            public DebugEvent(string name, int id, int subscriptions)
            {
                this.name = name;
                this.id = id;
                this.subscriptions = subscriptions;
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

                IReadOnlyDictionary<int, Delegate> events = _eventBus?.Events;
                if (events == null)
                    return _debugEventsCache;

                foreach ((int key, Delegate del) in events)
                {
                    string name = EventBusUtils.IdToName(key);
                    int subscriptions = del.GetInvocationList().Length;
                    _debugEventsCache.Add(new DebugEvent(name, key, subscriptions));
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
            if (_eventBus != null) _eventBus.Dispose(debugEvent.id);
        }

        private void Debug_UndeclareEventAt(int index)
        {
            if (_eventBus != null) _eventBus.Dispose(this.DebugEvents[index].id);
        }
    }
}
#endif