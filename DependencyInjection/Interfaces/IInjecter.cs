namespace CoreSystems.DependencyInjection
{
    public interface IInjecter
    {
        void StoreDependency<T>(T dependency);
        void InjectAllDependenciesOnScene();
        void InstallAllDependenciesOnScene();
        object[] GetFromContext();
        T GetDependency<T>();
    }
}