namespace CoreSystems.HUDSystem
{
    internal interface IScreenManager
    {
        void OpenScreen<T>() where T: IScreen;
        void CloseScreen(IScreen screen = null);
    }
}
namespace CoreSystems.HUDSystem.Extended
{
    internal interface IScreenManager
    {
        void OpenScreen<T>(params object[] parameters) where T : IScreen;
        void CloseScreen(IScreen screen = null);
    }
}