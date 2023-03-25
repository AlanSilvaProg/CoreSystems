namespace CoreSystems.DependencyInjection
{
    using UnityEngine;

    /// <summary>
    /// MonoInstaller only make a install to the corresponding injecter context
    /// </summary>
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        protected bool _installed = false;

        public abstract void InstallDependency(IInjecter injecter);
    }
}