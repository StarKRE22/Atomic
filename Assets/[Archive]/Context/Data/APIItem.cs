#if UNITY_EDITOR
using System;

namespace Atomic.Contexts
{
    [Serializable]
    internal sealed class APIItem : IComparable<APIItem>
    {
        public int id;
        public string name;
        public string type;

        public APIItem()
        {
        }

        public APIItem(int id, string name, string type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = id;
                hashCode = (hashCode * 397) ^ (name != null ? name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (type != null ? type.GetHashCode() : 0);
                return hashCode;
            }
        }

        public int CompareTo(APIItem other)
        {
            return this.id.CompareTo(other.id);
        }

        public override string ToString()
        {
            return $"{nameof(id)}: {id}, {nameof(name)}: {name}, {nameof(type)}: {type}";
        }
        
        private bool Equals(APIItem other)
        {
            return id == other.id && name == other.name && type == other.type;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is APIItem other && Equals(other);
        }
    }
}
#endif