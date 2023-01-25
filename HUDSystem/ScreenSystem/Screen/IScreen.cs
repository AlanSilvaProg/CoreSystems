namespace CoreSystems.HUDSystem
{
    public interface IScreen
    {
        void Open();
        void Close();
    }
}

namespace CoreSystems.HUDSystem.Extended
{
    public interface IScreen
    {
        void Open(params object[] parameters);
        void Close();
    }
}