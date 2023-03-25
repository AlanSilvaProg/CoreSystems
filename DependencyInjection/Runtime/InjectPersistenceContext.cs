namespace CoreSystems.DependencyInjection
{
    using System.Reflection;
    /// <summary>
    /// Persistence context means that all objects installed here will be persisten, so will receive the dont destroy on load function
    /// </summary>
    public class InjectPersistenceContext : InjectDependency
    {
        public override object[] GetFromContext() => ToInject.objs.ToArray();

        private void OnLevelWasLoaded(int level)
        {
            InjectAllDependenciesOnScene();
        }

        private void Start()
        {
            foreach (var install in _installed.Values)
                DontDestroyOnLoad((UnityEngine.Object)install);
            DontDestroyOnLoad(this);
        }

        public override void InjectAllDependenciesOnScene()
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
    }
}