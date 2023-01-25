namespace CoreSystems.HUDSystem
{
    public interface IScreen
    {
        void Open(IScreenManager manager);
        void Close();
    }
}

namespace CoreSystems.HUDSystem.Extended
{
    public interface IScreen
    {
        void Open(IScreenManager manager, params object[] parameters);
        void Close();
    }
}