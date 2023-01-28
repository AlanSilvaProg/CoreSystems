namespace CoreSystems.DependencyInjection
{
    using UnityEngine;
    using System.Collections.Generic;

    public static class ToInject
    {
        public static List<MonoBehaviour> objs = new List<MonoBehaviour>();
    }

    [DefaultExecutionOrder(-300)]
    public class MonoInject : MonoBehaviour, IInject
    {
        protected virtual void Awake()
        {
            ToInject.objs.Add(this);
        }

        private void OnDestroy()
        {
            ToInject.objs.Remove(this);
        }
    }

    [DefaultExecutionOrder(-300)]
    public class MonoInject<T> : MonoBehaviour, IInject
    {
        [Inject] protected T dependency;

        protected virtual void Awake()
        {
            ToInject.objs.Add(this);
        }

        private void OnDestroy()
        {
            ToInject.objs.Remove(this);
        }
    }
}