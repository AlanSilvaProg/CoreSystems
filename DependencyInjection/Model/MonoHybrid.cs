namespace CoreSystems.DependencyInjection
{
    using UnityEngine;
    /// <summary>
    /// MonoHybrid means that this object will be an installer in the same time that is an injecter
    /// </summary>
    [DefaultExecutionOrder(-300)]
    public abstract class MonoHybrid : MonoBehaviour, IHybrid
    {
        public abstract void InstallDependency(IInjecter injecter);

        protected virtual void Awake()
        {
            if (ToInject.objs == null) ToInject.Initialize();
            ToInject.objs.Add(this);
        }

        protected virtual void OnDestroy()
        {
            ToInject.objs.Remove(this);
        }
    }
}