#if UNITY_EDITOR && ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [LabelText("Name")]
        private string NameDebug => _entity?.Name ?? this.name;

        [FoldoutGroup("Debug")]
        [LabelText("Initialized")]
        [ShowInInspector, ReadOnly]
        private bool InitializedDebug => _entity?.Initialized ?? false;

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly]
        [LabelText("Enabled")]
        private bool EnabledDebug => _entity?.Enabled ?? false;

        ///Tags
        private static readonly List<TagElement> _tagElememtsCache = new();

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

            public int CompareTo(TagElement other)
            {
                return string.Compare(this.name, other.name, StringComparison.Ordinal);
            }
        }

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

                IReadOnlyCollection<int> tags = _entity?.GetTags();
                if (tags == null)
                    return _tagElememtsCache;

                foreach (int tag in tags)
                {
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

        private void RemoveTagElement(TagElement tagElement)
        {
            if (_entity != null) this.DelTag(tagElement.id);
        }

        private void RemoveTagElementAt(int index)
        {
            if (_entity != null) this.DelTag(this.TagElememts[index].id);
        }

        ///Values
        private static readonly List<ValueElement> _valueElementsCache = new();

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

            public int CompareTo(ValueElement other)
            {
                return string.Compare(this.name, other.name, StringComparison.Ordinal);
            }
        }

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

                IEnumerable<KeyValuePair<int, object>> values = _entity?.GetValues();
                if (values == null)
                    return _valueElementsCache;

                foreach ((int id, object value) in values)
                {
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

        private void RemoveValueElement(ValueElement valueElement)
        {
            if (_entity != null) this.DelValue(valueElement.id);
        }

        private void RemoveValueElementAt(int index)
        {
            if (_entity != null) this.DelValue(this.ValueElements[index].id);
        }


        ///Behaviours
        private static readonly List<BehaviourElement> _behaviourElementsCache = new();

        [InlineProperty]
        private struct BehaviourElement : IComparable<BehaviourElement>
        {
            [ShowInInspector, ReadOnly]
            public string name;

            internal readonly IBehaviour value;

            public BehaviourElement(string name, IBehaviour value)
            {
                this.name = name;
                this.value = value;
            }

            public int CompareTo(BehaviourElement other)
            {
                return string.Compare(this.name, other.name, StringComparison.Ordinal);
            }
        }

        [Searchable]
        [FoldoutGroup("Debug")]
        [LabelText("Behaviours")]
        [ShowInInspector, PropertyOrder(100)]
        [ListDrawerSettings(
            CustomRemoveElementFunction = nameof(RemoveLogicElement),
            CustomRemoveIndexFunction = nameof(RemoveLogicElementAt),
            HideAddButton = true
        )]
        private List<BehaviourElement> BehaviourElements
        {
            get
            {
                _behaviourElementsCache.Clear();

                IReadOnlyCollection<IBehaviour> behaviours = _entity?.GetBehaviours();
                if (behaviours == null)
                    return _behaviourElementsCache;

                foreach (IBehaviour behaviour in behaviours)
                {
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

        private void RemoveLogicElement(BehaviourElement behaviourElement)
        {
            if (_entity != null) this.DelBehaviour(behaviourElement.value);
        }

        private void RemoveLogicElementAt(int index)
        {
            if (_entity != null) this.DelBehaviour(this.BehaviourElements[index].value);
        }

        ///Add Element 
        [PropertySpace]
        [FoldoutGroup("Debug")]
        [Button("Install")]
        [ShowInInspector, PropertyOrder(100), HideInEditorMode]
        private void InstallDebug(IEntityInstaller installer)
        {
            installer?.Install(this);
        }
    }
}
#endif
