namespace EssentialSystems
{
    public interface IServiceLocator
    {
        T GetService<T>();
        void RegisterService<T>(object service);
    }
}