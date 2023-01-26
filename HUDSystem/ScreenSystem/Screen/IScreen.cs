namespace CoreSystems.HUDSystem
{
    public interface IScreen
    {
        void SetupButtons();
        void Open(IScreenManager manager);
        void Open(IScreenManager manager, params object[] parameters);
        void Close();
        void ChangeButtonState(bool state);
    }
}