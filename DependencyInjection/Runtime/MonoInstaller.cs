namespace CoreSystems.DependencyInjection
{
    using UnityEngine;
    using CoreSystems.ServiceLocator;

    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private bool _persistentInstaller;
        protected bool _installed = false;

        protected virtual void Awake()
        {
            if (!_installed)
            {
                InstallDependency(PublicServiceLocator.s_serviceLocator.GetService<IInjecter>());
            }
            if (_persistentInstaller)
            {
                PersistentDependencies.dependencies.Add(this);
                DontDestroyOnLoad(this);
            }
        }

        public abstract void InstallDependency(IInjecter injecter);
    }
}