namespace CoreSystems.ServiceLocator
{
    public interface IServiceLocator
    {
        T GetService<T>();
        void RegisterService<T>(object service);
    }
}