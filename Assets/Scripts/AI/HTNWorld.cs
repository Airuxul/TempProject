using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices.ComTypes;

namespace AI
{
    public static class HTNWorld
    {
        private static readonly Dictionary<string, Func<object>> GetWorldState;
        private static readonly Dictionary<string, Action<object>> SetWorldState;

        static HTNWorld()
        {
            GetWorldState = new Dictionary<string, Func<object>>();
            SetWorldState = new Dictionary<string, Action<object>>();
        }

        public static void AddState(string key, Func<object> getter, Action<object> setter)
        {
            GetWorldState[key] = getter;
            SetWorldState[key] = setter;
        }

        public static void RemoveState(string key)
        {
            GetWorldState.Remove(key);
            SetWorldState.Remove(key);
        }
        
        public static void UpdateState(string key, object value)
        {
            SetWorldState[key]?.Invoke(value);
        }
        
        public static T GetState<T>(string key)
        {
            return (T) GetWorldState[key]?.Invoke();
        }
        
        public static Dictionary<string, object> CopyWorldState()
        {
            var copy = new Dictionary<string, object>();
            foreach (var key in GetWorldState.Keys)
            {
                copy[key] = GetWorldState[key]?.Invoke();
            }
            return copy;
        }
    }
}