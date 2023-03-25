namespace CoreSystems.DependencyInjection
{
    /// <summary>
    /// Scene Context means that all objects with [Inject] will be injected with his installers 
    /// </summary>
    public class InjectSceneContext : InjectDependency
    {
        public override object[] GetFromContext() => ToInject.objs.ToArray();
    }
}