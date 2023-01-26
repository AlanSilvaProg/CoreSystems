namespace CoreSystems.HUDSystem
{
    public interface IScreen
    {
        void Open(IScreenManager manager);
        void Open(IScreenManager manager, params object[] parameters);
        void Close();
    }
}