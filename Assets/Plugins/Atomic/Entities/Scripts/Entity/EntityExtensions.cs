using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public static void AddBehaviours(this IEntity entity, in IEnumerable<IBehaviour> behaviours)
        {
            if (behaviours == null)
                return;

            foreach (IBehaviour behaviour in behaviours)
                entity.AddBehaviour(behaviour);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelBehaviours(this IEntity entity, in IEnumerable<IBehaviour> behaviours)
        {
            if (behaviours == null)
                return;

            foreach (IBehaviour behaviour in behaviours)
                entity.DelBehaviour(behaviour);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviour<T>(this IEntity it) where T : IBehaviour, new()
        {
            it.AddBehaviour(new T());
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
        public static bool TryGetEntity(this GameObject gameObject, out IEntity obj) =>
            gameObject.TryGetComponent(out obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Component component, out IEntity obj) =>
            component.TryGetComponent(out obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision2D collision2D, out IEntity obj) =>
            collision2D.gameObject.TryGetComponent(out obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision collision, out IEntity obj) =>
            collision.gameObject.TryGetComponent(out obj);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this GameObject gameObject, out IEntity obj)
        {
            obj = gameObject.GetComponentInParent<IEntity>();
            return obj != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Component component, out IEntity obj)
        {
            obj = component.GetComponentInParent<IEntity>();
            return obj != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Collision2D collision2D, out IEntity obj)
        {
            obj = collision2D.gameObject.GetComponentInParent<IEntity>();
            return obj != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Collision collision, out IEntity obj)
        {
            obj = collision.gameObject.GetComponentInParent<IEntity>();
            return obj != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEntity Install(this IEntity obj, in IEntityInstaller installer)
        {
            installer.Install(obj);
            return obj;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallFromScene(this IEntity obj, Scene scene, bool includeInactive = true)
        {
            foreach (GameObject gameObject in scene.GetRootGameObjects())
            {
                SceneEntityInstaller[] installers = gameObject
                    .GetComponentsInChildren<SceneEntityInstaller>(includeInactive);
              
                foreach (SceneEntityInstaller installer in installers) 
                    installer.Install(obj);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTag(this IEntity entity, string tag) => entity.HasTag(NameToId(tag));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag(this IEntity entity, string tag) => entity.AddTag(NameToId(tag));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTag(this IEntity entity, string tag) => entity.DelTag(NameToId(tag));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IEntity entity, string key) => entity.GetValue<T>(NameToId(key));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IEntity entity, string key, out T value) =>
            entity.TryGetValue(NameToId(key), out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue(this IEntity entity, string key, object value) =>
            entity.AddValue(NameToId(key), value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue<T>(this IEntity entity, string key, T value) where T : struct =>
            entity.AddValue(NameToId(key), value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue(this IEntity entity, string key) =>
            entity.DelValue(NameToId(key));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue(this IEntity entity, string key, object value) =>
            entity.SetValue(NameToId(key), value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<T>(this IEntity entity, string key, T value) where T : struct =>
            entity.SetValue(NameToId(key), value);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue(this IEntity entity, string key) => entity.HasValue(NameToId(key));

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

        public static void DisposeValues(this IEntity entity)
        {
            var values = entity.GetValues();
            foreach (KeyValuePair<int, object> pair in values)
                if (pair.Value is IDisposable disposable)
                    disposable.Dispose();
        }
    }
}