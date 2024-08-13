using System.Runtime.CompilerServices;

namespace Atomic.Contexts
{
    public static class ResolveCases
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object ResolveValue(this IContext context, int key)
        {
            if (context.TryGetValue(key, out object value))
            {
                return value;
            }

            while (true)
            {
                context = context.Parent;
                
                if (context == null)
                {
                    return null;
                }

                value = context.GetValue(key);

                if (value != null)
                {
                    return value;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ResolveValue<T>(this IContext context, int key) where T : class
        {
            if (context.TryGetValue(key, out T value))
            {
                return value;
            }

            while (true)
            {
                context = context.Parent;
                
                if (context == null)
                {
                    return null;
                }

                value = context.GetValue<T>(key);

                if (value != null)
                {
                    return value;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryResolveValue<T>(this IContext context, int key, out T value) where T : class
        {
            if (context.TryGetValue(key, out value))
            {
                return true;
            }

            while (true)
            {
                context = context.Parent;
                
                if (context == null)
                {
                    return false;
                }

                value = context.GetValue<T>(key);

                if (value != null)
                {
                    return true;
                }
            }
        }
    }
}