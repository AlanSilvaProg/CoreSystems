namespace CoreSystems.DependencyInjection
{
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// All objects that need injects
    /// </summary>
    public static class ToInject
    {
        public static List<MonoBehaviour> objs;

        public static void Initialize() => objs = new List<MonoBehaviour>();
    }

    /// <summary>
    /// MonoInject means that component need one or more injections
    /// </summary>
    [DefaultExecutionOrder(-300)]
    public class MonoInject : MonoBehaviour, IInject
    {
        protected virtual void Awake()
        {
            if (ToInject.objs == null) ToInject.Initialize();
            ToInject.objs.Add(this);
        }

        private void OnDestroy()
        {
            ToInject.objs.Remove(this);
        }
    }

    /// <summary>
    /// MonoInject<typeparamref name="T"/> means that component need only one injection
    /// </summary>
    [DefaultExecutionOrder(-300)]
    public class MonoInject<T> : MonoBehaviour, IInject
    {
        [Inject] protected T dependency;

        protected virtual void Awake()
        {
            if (ToInject.objs == null) ToInject.Initialize();
            ToInject.objs.Add(this);
        }

        private void OnDestroy()
        {
            ToInject.objs.Remove(this);
        }
    }
}