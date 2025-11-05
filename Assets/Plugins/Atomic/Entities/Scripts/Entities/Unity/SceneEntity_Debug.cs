#if UNITY_EDITOR && ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides inspector-only debug UI for the <see cref="SceneEntity"/> including read-only state
    /// and editable lists for tags, values, and behaviors.
    /// </summary>
    public partial class SceneEntity
    {
        #region Tags

        private static readonly List<DebugTag> _debugTagsCache = new();

        /// <summary>
        /// Represents a tag element with its display name and internal ID.
        /// </summary>
        [InlineProperty]
        private struct DebugTag : IComparable<DebugTag>
        {
            [ShowInInspector, ReadOnly]
            internal string name;
            internal readonly int id;

            public DebugTag(string name, int id)
            {
                this.name = name;
                this.id = id;
            }

            public int CompareTo(DebugTag other) => string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets a sorted list of tag elements currently assigned to the entity.
        /// </summary>
        [Searchable]
        [FoldoutGroup("Debug", order: 2)]
        [LabelText("Tags")]
        [ShowInInspector]
        [PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveDebugTag),
            CustomRemoveIndexFunction = nameof(RemoveDebugTagAt),
            HideAddButton = true
        )]
        private List<DebugTag> DebugTags
        {
            get
            {
                _debugTagsCache.Clear();

                TagEnumerator tagEnumerator = this.GetTagEnumerator();
                while (tagEnumerator.MoveNext())
                {
                    int tag = tagEnumerator.Current;
                    string name = EntityNames.IdToName(tag);
                    _debugTagsCache.Add(new DebugTag(name, tag));
                }

                _debugTagsCache.Sort();
                return _debugTagsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void RemoveDebugTag(DebugTag debugTag) => this.DelTag(debugTag.id);

        private void RemoveDebugTagAt(int index) => this.DelTag(this.DebugTags[index].id);

        #endregion

        #region Values

        private static readonly List<DebugValue> _debugValuesCache = new();

        /// <summary>
        /// Represents a value element consisting of a name, object value, and internal key.
        /// </summary>
        [InlineProperty]
        private struct DebugValue : IComparable<DebugValue>
        {
            [HorizontalGroup(200), ShowInInspector, ReadOnly, HideLabel]
            public string name;

            [HorizontalGroup, ShowInInspector, HideLabel]
            public object value;

            internal readonly int id;

            public DebugValue(string name, object value, int id)
            {
                this.name = name;
                this.value = value;
                this.id = id;
            }

            public int CompareTo(DebugValue other) =>
                string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets a sorted list of values currently stored in the entity.
        /// </summary>
        [Searchable]
        [FoldoutGroup("Debug", order: 3)]
        [LabelText("Values")]
        [ShowInInspector]
        [PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveDebugValue),
            CustomRemoveIndexFunction = nameof(RemoveDebugValueAt),
            HideAddButton = true
        )]
        private List<DebugValue> DebugValues
        {
            get
            {
                _debugValuesCache.Clear();

                ValueEnumerator enumerator = this.GetValueEnumerator();
                while (enumerator.MoveNext())
                {
                    (int id, object value) = enumerator.Current;
                    string name = EntityNames.IdToName(id);
                    _debugValuesCache.Add(new DebugValue(name, value, id));
                }

                _debugValuesCache.Sort();
                return _debugValuesCache;
            }

            set
            {
                /** noting... **/
            }
        }

        private void RemoveDebugValue(DebugValue debugValue) =>
            this.DelValue(debugValue.id);

        private void RemoveDebugValueAt(int index) =>
            this.DelValue(this.DebugValues[index].id);

        #endregion

        #region Behaviours

        private static readonly List<DebugBehaviour> _debugBehavioursCache = new();

        /// <summary>
        /// Represents a behaviour component with a name and IBehaviour instance.
        /// </summary>
        [InlineProperty]
        private struct DebugBehaviour : IComparable<DebugBehaviour>
        {
            [ShowInInspector, ReadOnly]
            public string name;

            internal readonly IEntityBehaviour value;

            public DebugBehaviour(string name, IEntityBehaviour value)
            {
                this.name = name;
                this.value = value;
            }

            public int CompareTo(DebugBehaviour other) =>
                string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets a sorted list of behaviours currently attached to the entity.
        /// </summary>
        [Searchable]
        [FoldoutGroup("Debug", order: 4)]
        [LabelText("Behaviours")]
        [ShowInInspector]
        [PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveDebugBehaviour),
            CustomRemoveIndexFunction = nameof(RemoveDebugBehaviourAt),
            HideAddButton = true
        )]
        private List<DebugBehaviour> DebugBehaviours
        {
            get
            {
                _debugBehavioursCache.Clear();

                for (int i = 0; i < _behaviourCount; i++)
                {
                    IEntityBehaviour behaviour = _behaviours[i];
                    string name = behaviour.GetType().Name;
                    _debugBehavioursCache.Add(new DebugBehaviour(name, behaviour));
                }

                _debugBehavioursCache.Sort();
                return _debugBehavioursCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void RemoveDebugBehaviour(DebugBehaviour debugBehaviour) =>
            this.DelBehaviour(debugBehaviour.value);

        private void RemoveDebugBehaviourAt(int index) =>
            this.DelBehaviour(this.DebugBehaviours[index].value);

        #endregion
    }
}
#endif