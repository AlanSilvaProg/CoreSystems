namespace CoreSystems.HUDSystem
{
    public interface IScreenManager
    {
        void OpenScreen<T>() where T: IScreen, IPopup;
        void OpenScreen<T>(params object[] parameters) where T : IScreen, IPopup;
        void CloseScreen(IScreen screen = null);
    }
}