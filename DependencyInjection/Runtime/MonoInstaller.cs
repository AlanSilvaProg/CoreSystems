namespace EssentialSystems
{
    using UnityEngine;

    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        protected bool _installed = false;

        public virtual void Awake()
        {
            if (!_installed)
            {
                InstallDependency(PublicServiceLocator.s_serviceLocator.GetService<IInjecter>());
            }
        }

        public abstract void InstallDependency(IInjecter injecter);
    }
}