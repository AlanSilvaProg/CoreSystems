namespace CoreSystems.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using CoreSystems.ServiceLocator;
    using CoreSystems.ExecutionOrderModifier;

    [ScriptOrder(-20)]
    public class InjectDependency : MonoBehaviour, IInjecter
    {
        [SerializeField] private MonoInstaller[] _installers;
        private Dictionary<object, object> _installed = new Dictionary<object, object>();
        private List<Action> dependencyInjections = new List<Action>();

        private void Awake()
        {
            PublicServiceLocator.s_serviceLocator.RegisterService<IInjecter>(this);
            InjectAllDependenciesOnScene();
        }

        public void StoreDependency<T>(T dependency)
        {
            if (_installed.ContainsKey(typeof(T)))
            {
                throw new Exception("Two or more dependencies has trying to be installed");
            }
            else
                _installed.Add(typeof(T), dependency);
        }

        public void InjectAllDependenciesOnScene()
        {
            foreach (IInstaller installer in _installers)
                installer.InstallDependency(this);
        }

        public T GetDependency<T>()
        {
            return (T)_installed[typeof(T)];
        }
    }
}