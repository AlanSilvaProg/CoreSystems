namespace CoreSystems.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    [DefaultExecutionOrder(-200)]
    public class InjectDependency : MonoBehaviour, IInjecter
    {
        private enum Context { SCENE, PERSISTENCE, GAMEOBJECT }
        [SerializeField] private Context _context;
        [SerializeField] private MonoInstaller[] _installers;
        private Dictionary<object, object> _installed = new Dictionary<object, object>();

        private bool _isPersistenceContext => _context == Context.PERSISTENCE;
        private bool _isGameObjectContext => _context == Context.GAMEOBJECT;

        private void OnLevelWasLoaded(int level)
        {
            InjectAllDependenciesOnScene();
        }

        private void Awake()
        {
            InstallAllDependenciesOnScene();
            InjectAllDependenciesOnScene();
            if (_isPersistenceContext)
                DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

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
            foreach (object obj in GetFromContext())
            {
                var fields = obj.GetType().GetFields();
                foreach (var field in fields)
                {
                    foreach (var dependant in _installed.Values)
                    {
                        var customField = field.GetCustomAttribute<Inject>();
                        if (customField == null || (customField.Injected && !_isGameObjectContext)) { continue; }
                        field.SetValue(obj, dependant);
                        customField.Injected = true;
                    }
                }
                if (_isPersistenceContext)
                    DontDestroyOnLoad((UnityEngine.Object)obj);
            }
        }

        public void InstallAllDependenciesOnScene()
        {
            foreach (IInstaller installer in _installers)
                installer.InstallDependency(this);
        }

        public T GetDependency<T>()
        {
            return (T)_installed[typeof(T)];
        }

        public object[] GetFromContext()
        {
            if (_context == Context.SCENE || _isPersistenceContext)
                return ToInject.objs.ToArray();
            else
            {
                List<IInject> components = new List<IInject>();
                GetComponentsInChildren(components);
                GetComponents(components);
                return components.ToArray();
            }
        }
    }
}