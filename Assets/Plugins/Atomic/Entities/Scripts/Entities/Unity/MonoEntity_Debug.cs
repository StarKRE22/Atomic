#if UNITY_EDITOR && ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides inspector-only debug UI for the <see cref="MonoEntity"/> including read-only state
    /// and editable lists for tags, values, and behaviors.
    /// </summary>
    public partial class MonoEntity
    {
        #region Tags

        private static readonly List<DebugTag> _debugTagsCache = new();

        /// <summary>
        /// Represents a tag element with its display name and internal ID.
        /// </summary>
        [InlineProperty]
        private readonly struct DebugTag : IComparable<DebugTag>
        {
            [ShowInInspector, ReadOnly]
            internal readonly string name;
            internal readonly int id;

            public DebugTag(int id)
            {
                this.name = EntityKeyStore.IdToName(id);
                this.id = id;
            }

            public int CompareTo(DebugTag other)
            {
                return string.Compare(this.name, other.name, StringComparison.Ordinal);
            }
        }

        /// <summary>
        /// Gets a sorted list of tag elements currently assigned to the entity.
        /// </summary>
        [Searchable]
        [PropertySpace]
        [FoldoutGroup("Debug", order: 2)]
        [LabelText("Tags")]
        [ShowInInspector]
        [PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveDebugTag),
            CustomRemoveIndexFunction = nameof(RemoveDebugTagAt),
            HideAddButton = true,
            DraggableItems = false
        )]
        private List<DebugTag> DebugTags
        {
            get
            {
                _debugTagsCache.Clear();

                TagEnumerator enumerator = new TagEnumerator(this);
                while (enumerator.MoveNext()) 
                    _debugTagsCache.Add(new DebugTag(enumerator.Current));

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
        
        [FoldoutGroup("Debug", order: 10)]
        [PropertyOrder(101)]
        [HorizontalGroup("Debug/AddTag", Width = 120)]
        [HideInEditorMode]
        [Button("Add Tag")]
        private void DebugAddTag() => this.AddTag(_debugTag);

        [FoldoutGroup("Debug", order: 10)]
        [PropertyOrder(102)]
        [HorizontalGroup("Debug/AddTag")]
        [HideInEditorMode]
        [ShowInInspector]
        [HideLabel]
        private string _debugTag;

        #endregion

        #region Values

        private static readonly List<DebugValue> _debugValuesCache = new();

        /// <summary>
        /// Represents a value element consisting of a name, object value, and internal key.
        /// </summary>
        [InlineProperty]
        private readonly struct DebugValue : IComparable<DebugValue>
        {
            [HorizontalGroup(200), ShowInInspector, HideLabel, ReadOnly]
            internal readonly string name;

            [HideReferenceObjectPicker]
            [HorizontalGroup, ShowInInspector, HideLabel]
            internal readonly object value;
            internal readonly int id;

            public DebugValue(KeyValuePair<int, object> pair) : this(pair.Key, pair.Value)
            {
            }
            
            public DebugValue(int id, object value)
            {
                this.name = EntityKeyStore.IdToName(id);
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
        [PropertySpace]
        [FoldoutGroup("Debug", order: 3)]
        [LabelText("Values")]
        [ShowInInspector]
        [PropertyOrder(200)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveDebugValue),
            CustomRemoveIndexFunction = nameof(RemoveDebugValueAt),
            HideAddButton = true,
            DraggableItems = false
        )]
        private List<DebugValue> DebugValues
        {
            get
            {
                _debugValuesCache.Clear();

                ValueEnumerator enumerator = this.GetValueEnumerator();
                while (enumerator.MoveNext()) 
                    _debugValuesCache.Add(new DebugValue(enumerator.Current));

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
        
        [FoldoutGroup("Debug", order: 10)]
        [PropertyOrder(201)]
        [HorizontalGroup("Debug/AddValue", Width = 120)]
        [HideInEditorMode]
        [Button("Add Value")]
        private void DebugAddValue() => this.AddValue(_debugValueKey, _debugValue);

        [FoldoutGroup("Debug", order: 10)]
        [PropertyOrder(202)]
        [HorizontalGroup("Debug/AddValue")]
        [HideInEditorMode]
        [ShowInInspector]
        [HideLabel]
        private string _debugValueKey;
        
        [FoldoutGroup("Debug", order: 10)]
        [PropertyOrder(1001)]
        [HorizontalGroup("Debug/AddValue")]
        [HideInEditorMode]
        [ShowInInspector]
        [HideLabel]
        private object _debugValue;

        #endregion

        #region Behaviours

        private static readonly List<DebugBehaviour> _debugBehavioursCache = new();

        /// <summary>
        /// Represents a behaviour component with a name and IBehaviour instance.
        /// </summary>
        [InlineProperty]
        private struct DebugBehaviour : IComparable<DebugBehaviour>
        {
            [ShowInInspector, HideLabel, ReadOnly]
            internal string name;
            internal readonly IEntityBehaviour behaviour;

            public DebugBehaviour(IEntityBehaviour behaviour)
            {
                this.name = behaviour.GetType().Name;
                this.behaviour = behaviour;
            }

            public int CompareTo(DebugBehaviour other) =>
                string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets a sorted list of behaviours currently attached to the entity.
        /// </summary>
        [Searchable]
        [PropertySpace]
        [FoldoutGroup("Debug", order: 4)]
        [LabelText("Behaviours")]
        [ShowInInspector]
        [PropertyOrder(300)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveDebugBehaviour),
            CustomRemoveIndexFunction = nameof(RemoveDebugBehaviourAt),
            HideAddButton = true,
            DraggableItems = false
        )]
        private List<DebugBehaviour> DebugBehaviours
        {
            get
            {
                _debugBehavioursCache.Clear();

                for (int i = 0; i < _behaviourCount; i++) 
                    _debugBehavioursCache.Add(new DebugBehaviour(_behaviours[i]));

                return _debugBehavioursCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void RemoveDebugBehaviour(DebugBehaviour debugBehaviour) =>
            this.DelBehaviour(debugBehaviour.behaviour);

        private void RemoveDebugBehaviourAt(int index) =>
            this.DelBehaviour(this.DebugBehaviours[index].behaviour);

        [FoldoutGroup("Debug", order: 10)]
        [PropertyOrder(301)]
        [HorizontalGroup("Debug/AddBehaviour", Width = 120)]
        [HideInEditorMode]
        [Button("Add Behaviour")]
        private void DebugAddBehaviour() => this.AddBehaviour(_debugBehaviour);

        [FoldoutGroup("Debug", order: 10)]
        [PropertyOrder(302)]
        [HorizontalGroup("Debug/AddBehaviour")]
        [HideInEditorMode]
        [ShowInInspector]
        [HideLabel]
        private IEntityBehaviour _debugBehaviour;
        
        #endregion

        #region Installer

        [PropertySpace(16)]
        [FoldoutGroup("Debug", order: 10)]
        [PropertyOrder(1000)]
        [HorizontalGroup("Debug/AddInstaller", Width = 120)]
        [HideInEditorMode]
        [Button("Add Installer")]
        private void DebugAddInstaller() => _debugInstaller?.Install(this);

        [PropertySpace(16)]
        [FoldoutGroup("Debug", order: 10)]
        [PropertyOrder(1001)]
        [HorizontalGroup("Debug/AddInstaller")]
        [HideInEditorMode]
        [ShowInInspector]
        [HideLabel]
        private IEntityInstaller _debugInstaller;
        
        #endregion
    }
}
#endif