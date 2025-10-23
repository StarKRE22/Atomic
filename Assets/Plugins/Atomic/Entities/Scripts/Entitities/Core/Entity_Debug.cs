#if UNITY_EDITOR && ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic.Entities
{
    public partial class Entity
    {
        /// <summary>
        /// Debug-only: Gets or sets the debug display name of the entity.
        /// </summary>
        [ShowInInspector]
        [LabelText("Name")]
        private string DebugName
        {
            get => this.Name;
            set => this.Name = value;
        }

        /// <summary>
        /// Debug-only: Indicates whether the entity has been initialized.
        /// </summary>
        [LabelText("Initialized")]
        [ShowInInspector]
        private bool DebugInitialized => this.Initialized;

        /// <summary>
        /// Debug-only: Indicates whether the entity is currently enabled.
        /// </summary>
        [ShowInInspector]
        [LabelText("Enabled")]
        private bool DebugEnabled => this.Enabled;

        #region Tags

        /// <summary>
        /// Represents a debug view of a tag.
        /// </summary>
        [InlineProperty]
        private readonly struct DebugTag : IComparable<DebugTag>
        {
            [ShowInInspector, ReadOnly]
            internal readonly string name;
            internal readonly int id;

            public DebugTag(string name, int id)
            {
                this.name = name;
                this.id = id;
            }

            public int CompareTo(DebugTag other) => string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Debug-only: Returns a sorted list of tag representations for display.
        /// </summary>
        [Searchable]
        [LabelText("Tags")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(DebugDelTag),
            CustomRemoveIndexFunction = nameof(DebugDelTagAt),
            HideAddButton = true
        )]
        private List<DebugTag> DebugTags
        {
            get
            {
                var result = new List<DebugTag>();
                
                foreach (int tag in this.GetTags())
                {
                    string name = EntityNames.IdToName(tag);
                    result.Add(new DebugTag(name, tag));
                }

                result.Sort();
                return result;
            }
            set
            {
                /** noting... **/
            }
        }

        private void DebugDelTag(DebugTag debugTag) => this.DelTag(debugTag.id);

        private void DebugDelTagAt(int index) => this.DelTag(this.DebugTags[index].id);

        #endregion

        #region Values

        [InlineProperty]
        private struct DebugValue : IComparable<DebugValue>
        {
            [HorizontalGroup(200), ShowInInspector, ReadOnly, HideLabel]
            public readonly string name;

            [HorizontalGroup, ShowInInspector, HideLabel]
            public object value;

            internal readonly int id;

            public DebugValue(string name, object value, int id)
            {
                this.name = name;
                this.value = value;
                this.id = id;
            }

            public int CompareTo(DebugValue other) => string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Debug-only: Returns a sorted list of values associated with the entity.
        /// </summary>
        [Searchable]
        [LabelText("Values")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(DebugDelValue),
            CustomRemoveIndexFunction = nameof(DebugDelValueAt),
            HideAddButton = true
        )]
        private List<DebugValue> DebugValues
        {
            get
            {
                List<DebugValue> result = new List<DebugValue>();
                IEnumerable<KeyValuePair<int, object>> values = this.GetValues();
                if (values == null)
                    return result;

                foreach ((int id, object value) in values)
                {
                    string name = EntityNames.IdToName(id);
                    result.Add(new DebugValue(name, value, id));
                }

                result.Sort();
                return result;
            }

            set
            {
                /** noting... **/
            }
        }

        private void DebugDelValue(DebugValue value) => this.DelValue(value.id);

        private void DebugDelValueAt(int index) => this.DelValue(this.DebugValues[index].id);

        #endregion

        #region Behaviours

        private static readonly List<DebugBehaviour> _debugBehavioursCache = new();

        /// <summary>
        /// Represents a debug view of a behaviour.
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

            public int CompareTo(DebugBehaviour other) => string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Debug-only: Returns a sorted list of behaviours currently attached to the entity.
        /// </summary>
        [Searchable]
        [LabelText("Behaviours")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(DebugDelBehaviour),
            CustomRemoveIndexFunction = nameof(DebugDelBehaviourAt),
            HideAddButton = true
        )]
        private List<DebugBehaviour> DebugBehaviours
        {
            get
            {
                _debugBehavioursCache.Clear();

                IReadOnlyCollection<IEntityBehaviour> behaviours = GetBehaviours();
                if (behaviours == null)
                    return _debugBehavioursCache;

                foreach (IEntityBehaviour behaviour in behaviours)
                {
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

        private void DebugDelBehaviour(DebugBehaviour behaviour) => this.DelBehaviour(behaviour.value);

        private void DebugDelBehaviourAt(int index) => this.DelBehaviour(this.DebugBehaviours[index].value);

        #endregion
    }
}
#endif