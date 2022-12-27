namespace EssentialSystems
{
    public interface IInjecter
    {
        void StoreDependency<T>(T dependency);
        void InjectAllDependenciesOnScene();
        T GetDependency<T>();
    }
}