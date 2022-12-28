namespace CoreSystems.DependencyInjection
{
    using UnityEngine;
    using CoreSystems.ServiceLocator;

    public class MonoInject<T> : MonoBehaviour, IInject
    {

        protected T dependency;

        protected virtual void Awake()
        {
            GetDependency();
        }

        public void GetDependency()
        {
            dependency = PublicServiceLocator.s_serviceLocator.GetService<IInjecter>().GetDependency<T>();
        }
    }
}