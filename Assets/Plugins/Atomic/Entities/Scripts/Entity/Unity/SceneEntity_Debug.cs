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
        /// <summary>
        /// Indicates whether the entity has been initialized.
        /// </summary>
      
        private bool InitializedDebug => this.Initialized;
        
        /// <summary>
        /// Indicates whether the entity is currently enabled.
        /// </summary>
       
        private bool EnabledDebug => this.Enabled;
        
        #region Tags

        private static readonly List<TagElement> _tagElememtsCache = new();

        /// <summary>
        /// Represents a tag element with its display name and internal ID.
        /// </summary>
        [InlineProperty]
        private struct TagElement : IComparable<TagElement>
        {
            [ShowInInspector, ReadOnly]
            internal string name;
            internal readonly int id;

            public TagElement(string name, int id)
            {
                this.name = name;
                this.id = id;
            }

            public int CompareTo(TagElement other) => string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets a sorted list of tag elements currently assigned to the entity.
        /// </summary>
        [Searchable]
        [FoldoutGroup("Debug")]
        [LabelText("Tags")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveTagElement),
            CustomRemoveIndexFunction = nameof(RemoveTagElementAt),
            HideAddButton = true
        )]
        private List<TagElement> TagElememts
        {
            get
            {
                _tagElememtsCache.Clear();

                TagEnumerator tagEnumerator = this.GetTagEnumerator();
                while (tagEnumerator.MoveNext())
                {
                    int tag = tagEnumerator.Current;
                    string name = EntityUtils.IdToName(tag);
                    _tagElememtsCache.Add(new TagElement(name, tag));
                }

                _tagElememtsCache.Sort();
                return _tagElememtsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void RemoveTagElement(TagElement tagElement) => this.DelTag(tagElement.id);

        private void RemoveTagElementAt(int index) => this.DelTag(this.TagElememts[index].id);

        #endregion

        #region Values

        private static readonly List<ValueElement> _valueElementsCache = new();

        /// <summary>
        /// Represents a value element consisting of a name, object value, and internal key.
        /// </summary>
        [InlineProperty]
        private struct ValueElement : IComparable<ValueElement>
        {
            [HorizontalGroup(200), ShowInInspector, ReadOnly, HideLabel]
            public string name;

            [HorizontalGroup, ShowInInspector, HideLabel]
            public object value;

            internal readonly int id;

            public ValueElement(string name, object value, int id)
            {
                this.name = name;
                this.value = value;
                this.id = id;
            }

            public int CompareTo(ValueElement other) => 
                string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets a sorted list of values currently stored in the entity.
        /// </summary>
        [Searchable]
        [FoldoutGroup("Debug")]
        [LabelText("Values")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveValueElement),
            CustomRemoveIndexFunction = nameof(RemoveValueElementAt),
            HideAddButton = true
        )]
        private List<ValueElement> ValueElements
        {
            get
            {
                _valueElementsCache.Clear();

                ValueEnumerator enumerator = this.GetValueEnumerator();
                while (enumerator.MoveNext())
                {
                    (int id, object value) = enumerator.Current;
                    string name = EntityUtils.IdToName(id);
                    _valueElementsCache.Add(new ValueElement(name, value, id));
                }

                _valueElementsCache.Sort();
                return _valueElementsCache;
            }

            set
            {
                /** noting... **/
            }
        }

        private void RemoveValueElement(ValueElement valueElement) => 
            this.DelValue(valueElement.id);

        private void RemoveValueElementAt(int index) => 
            this.DelValue(this.ValueElements[index].id);

        #endregion

        #region Behaviours

        private static readonly List<BehaviourElement> _behaviourElementsCache = new();

        /// <summary>
        /// Represents a behaviour component with a name and IBehaviour instance.
        /// </summary>
        [InlineProperty]
        private struct BehaviourElement : IComparable<BehaviourElement>
        {
            [ShowInInspector, ReadOnly]
            public string name;

            internal readonly IEntityBehaviour value;

            public BehaviourElement(string name, IEntityBehaviour value)
            {
                this.name = name;
                this.value = value;
            }

            public int CompareTo(BehaviourElement other) => string.Compare(this.name, other.name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets a sorted list of behaviours currently attached to the entity.
        /// </summary>
        [Searchable]
        [FoldoutGroup("Debug")]
        [LabelText("Behaviours")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveBehaviourElement),
            CustomRemoveIndexFunction = nameof(RemoveBehaviourElementAt),
            HideAddButton = true
        )]
        private List<BehaviourElement> BehaviourElements
        {
            get
            {
                _behaviourElementsCache.Clear();

                for (int i = 0; i < _behaviourCount; i++)
                {
                    IEntityBehaviour behaviour = _behaviours[i];
                    string name = behaviour.GetType().Name;
                    _behaviourElementsCache.Add(new BehaviourElement(name, behaviour));
                }

                _behaviourElementsCache.Sort();
                return _behaviourElementsCache;
            }
            set
            {
                /** noting... **/
            }
        }

        private void RemoveBehaviourElement(BehaviourElement behaviourElement) => 
            this.DelBehaviour(behaviourElement.value);

        private void RemoveBehaviourElementAt(int index) => 
            this.DelBehaviour(this.BehaviourElements[index].value);

        #endregion
    }
}
#endif
