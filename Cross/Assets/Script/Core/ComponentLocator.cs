using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{

    public static class ComponentLocator
    {
        private static readonly Dictionary<Type, IStaticCache> cache = new();
        private static readonly List<Type> compTypesToRemove = new();
        private const float GCInterval = 31f;
        private static float lastGCAt = 0f;
        
        public static bool UseGC { get; set; } = true;

        public static T GetOrNull<T>() where T : Component
        {
            GCIfNeeded();

            var value = StaticCache<T>.Value;
            if (value)
            {
                return value;
            }

            Uncache<T>();
            
#pragma warning disable CS0618 // 型またはメンバーが旧型式です
            value = Object.FindObjectOfType<T>();
#pragma warning restore CS0618 // 型またはメンバーが旧型式です
            if (value)
            {
                Cache(value);
                return value;
            }

            return null;
        }

        public static T Get<T>() where T : Component
        {
            var comp = GetOrNull<T>();
            if (comp) return comp;

            var compType = typeof(T);
            var gameObject = new GameObject(compType.Name, compType);
            comp = gameObject.GetComponent<T>();
            if (comp)
            {
                Uncache<T>();
                Cache(comp);
                return comp;
            }

            return null;
        }

        public static T FromResources<T>(string path) where T : Component
        {
            var comp = GetOrNull<T>();
            if (comp) return comp;

            var resource = Resources.Load<T>(path);
            if (resource)
            {
                comp = GameObject.Instantiate(resource);
                Uncache<T>();
                Cache(comp);
                return comp;
            }
            return null;
        }

        public static void Cache<T>(T value) where T : Component
        {
            StaticCache<T>.Value = value;
            cache.Add(typeof(T), StaticCache<T>.Instance);
        }

        public static void Uncache<T>() where T : Component
        {
            StaticCache<T>.Value = null;
            cache.Remove(typeof(T));
        }

        private static void GCIfNeeded()
        {
            if (!UseGC) return;

            var now = Time.unscaledTime;
            if (GCInterval < now - lastGCAt)
            {
                lastGCAt = now;
                GC();
            }
        }
        
        public static void GC()
        {
            foreach (var kv in cache)
            {
                if (kv.Value == null)
                {
                    compTypesToRemove.Add(kv.Key);
                }
            }
            foreach (var compType in compTypesToRemove)
            {
                cache.Remove(compType);
            }
            compTypesToRemove.Clear();
        }
    }

    internal interface IStaticCache
    {
        Component Component { get; }
    }

    internal class StaticCache<T> : IStaticCache where T : Component
    {
        internal static StaticCache<T> Instance = new();

        internal static T Value;
        public Component Component
        {
            get { return Value as Component; }
            set { Value = value as T; }
        }
    }
}