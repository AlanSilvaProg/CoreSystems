namespace CoreSystems.DependencyInjection
{
    public interface IInstaller
    {
        void InstallDependency(IInjecter injecter);
    }
}