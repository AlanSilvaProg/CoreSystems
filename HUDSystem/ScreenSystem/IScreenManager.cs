namespace CoreSystems.HUDSystem
{
    public interface IScreenManager
    {
        void OpenScreen<T>() where T: IScreen;
        void CloseScreen(IScreen screen = null);
    }
}
namespace CoreSystems.HUDSystem.Extended
{
    public interface IScreenManager
    {
        void OpenScreen<T>(params object[] parameters) where T : IScreen;
        void CloseScreen(IScreen screen = null);
    }
}