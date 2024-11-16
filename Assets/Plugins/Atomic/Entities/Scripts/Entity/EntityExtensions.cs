using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Atomic.Entities.EntityUtils;

namespace Atomic.Entities
{
    public static class EntityExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags(this IEntity entity, in IEnumerable<int> tags)
        {
            if (tags == null)
                return;

            foreach (int tag in tags) 
                entity.AddTag(tag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValues(this IEntity entity, in IReadOnlyDictionary<int, object> values)
        {
            if (values == null)
                return;

            foreach ((int key, object value) in values) 
                entity.AddValue(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviours(this IEntity entity, in IEnumerable<IEntityBehaviour> behaviours)
        {
            if (behaviours == null)
                return;

            foreach (IEntityBehaviour behaviour in behaviours) 
                entity.AddBehaviour(behaviour);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear(this IEntity entity)
        {
            entity.ClearTags();
            entity.ClearValues();
            entity.ClearBehaviours();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasBehaviour<T>(this IEntity it) where T : IEntityBehaviour
        {
            foreach (IEntityBehaviour behaviour in it.Behaviours)
                if (behaviour is T)
                    return true;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetBehaviour<T>(this IEntity it) where T : IEntityBehaviour
        {
            foreach (IEntityBehaviour behaviour in it.Behaviours)
                if (behaviour is T result)
                    return result;

            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetBehaviour<T>(this IEntity it, out T result) where T : IEntityBehaviour
        {
            foreach (IEntityBehaviour behaviour in it.Behaviours)
                if (behaviour is T tBehaviour)
                {
                    result = tBehaviour;
                    return true;
                }

            result = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviour<T>(this IEntity it) where T : IEntityBehaviour, new()
        {
            it.AddBehaviour(new T());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelBehaviour<T>(this IEntity it) where T : IEntityBehaviour
        {
            T behaviour = it.GetBehaviour<T>();
            if (behaviour == null)
                return false;

            return it.DelBehaviour(behaviour);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags(this IEntity obj, params int[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (!obj.HasTag(tags[i]))
                    return false;

            return true;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyTag(this IEntity obj, params int[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (obj.HasTag(tags[i]))
                    return true;

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this GameObject gameObject, out IEntity obj)
        {
            return gameObject.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Component component, out IEntity obj)
        {
            return component.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision2D collision2D, out IEntity obj)
        {
            return collision2D.gameObject.TryGetComponent(out obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision collision, out IEntity obj)
        {
            return collision.gameObject.TryGetComponent(out obj);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComponent(this IEntity obj, int id, IEntityBehaviour element)
        {
            if (obj.AddValue(id, element)) 
                obj.AddBehaviour(element);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelComponent(this IEntity obj, int id)
        {
            if (obj.DelValue(id, out var removed)) 
                obj.DelBehaviour(removed as IEntityBehaviour);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetComponent(this IEntity obj, int id, IEntityBehaviour element)
        {
            obj.SetValue(id, element);
            obj.AddBehaviour(element);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEntity Install(this IEntity obj, IEntityInstaller installer)
        {
            installer.Install(obj);
            return obj;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTag(this IEntity entity, string tag)
        {
            return entity.HasTag(NameToId(tag));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag(this IEntity entity, string tag)
        {
            return entity.AddTag(NameToId(tag));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTag(this IEntity entity, string tag)
        {
            return entity.DelTag(NameToId(tag));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IEntity entity, string key)
        {
            return entity.GetValue<T>(NameToId(key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetValue(this IEntity entity, string key)
        {
            return entity.GetValue(NameToId(key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IEntity entity, string key, out T value)
        {
            return entity.TryGetValue(NameToId(key), out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue(this IEntity entity, string key, out object value)
        {
            return entity.TryGetValue(NameToId(key), out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddValue(this IEntity entity, string key, object value)
        {
            return entity.AddValue(NameToId(key), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue(this IEntity entity, string key)
        {
            return entity.DelValue(NameToId(key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue(this IEntity entity, string key, out object removed)
        {
            return entity.DelValue(NameToId(key), out removed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue(this IEntity entity, string key, object value)
        {
            entity.SetValue(NameToId(key), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue(this IEntity entity, string key, object value, out object previous)
        {
            entity.SetValue(NameToId(key), value, out previous);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue(this IEntity entity, string key)
        {
            return entity.HasValue(NameToId(key));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags(this IEntity entity, IEnumerable<string> tags)
        {
            if (tags == null)
                return;

            foreach (string tag in tags) 
                entity.AddTag(tag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValues(this IEntity entity, IReadOnlyDictionary<string, object> values)
        {
            if (values == null)
                return;

            foreach ((string key, object value) in values) 
                entity.AddValue(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags(this IEntity obj, params string[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (!obj.HasTag(tags[i]))
                    return false;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyTag(this IEntity obj, params string[] tags)
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (obj.HasTag(tags[i]))
                    return true;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddComponent(this IEntity obj, string id, IEntityBehaviour element)
        {
            if (obj.AddValue(id, element))
                obj.AddBehaviour(element);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelComponent(this IEntity obj, string id)
        {
            if (obj.DelValue(id, out object removed)) 
                obj.DelBehaviour(removed as IEntityBehaviour);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetComponent(this IEntity obj, string id, IEntityBehaviour element)
        {
            obj.SetValue(id, element);
            obj.AddBehaviour(element);
        }
    }
}