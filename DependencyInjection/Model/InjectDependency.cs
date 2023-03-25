namespace CoreSystems.DependencyInjection
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;

    [DefaultExecutionOrder(-200)]
    public abstract class InjectDependency : MonoBehaviour, IInjecter
    {
        [SerializeField] protected MonoInstaller[] _monoInstallers;
        [SerializeField] protected MonoHybrid[] _hybridInstallers;
        protected Dictionary<object, object> _installed = new Dictionary<object, object>();

        [RuntimeInitializeOnLoadMethod]
        protected virtual void Awake()
        {
            InstallAllDependenciesOnScene();
            InjectAllDependenciesOnScene();
        }

        /// <summary>
        /// Store the installer reference
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dependency"></param>
        public virtual void StoreDependency<T>(T dependency)
        {
            if (_installed.ContainsKey(typeof(T)))
            {
                throw new Exception("Two or more dependencies has trying to be installed");
            }
            else
                _installed.Add(typeof(T), dependency);
        }

        /// <summary>
        /// Inject the exist installers in all of receivers 
        /// </summary>
        public virtual void InjectAllDependenciesOnScene()
        {
            foreach (object obj in GetFromContext())
            {
                var fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    var customField = field.GetCustomAttribute<Inject>();
                    if (customField == null) { continue; }
                    if (_installed.ContainsKey(field.FieldType) && _installed[field.FieldType] != null)
                    {
                        field.SetValue(obj, _installed[field.FieldType]);
                    }
                }
            }
        }

        public void InstallAllDependenciesOnScene()
        {
            foreach (IInstaller installer in _monoInstallers)
                installer.InstallDependency(this);
            foreach (IHybrid installer in _hybridInstallers)
                installer.InstallDependency(this);
        }

        public T GetDependency<T>()
        {
            return (T)_installed[typeof(T)];
        }

        public abstract object[] GetFromContext();
    }
}