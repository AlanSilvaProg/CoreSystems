namespace CoreSystems.DependencyInjection
{
    using UnityEngine;

    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        protected bool _installed = false;

        public abstract void InstallDependency(IInjecter injecter);
    }
}