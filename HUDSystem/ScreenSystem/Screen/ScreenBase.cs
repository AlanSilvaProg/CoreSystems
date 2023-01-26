namespace CoreSystems.HUDSystem
{
    using UnityEngine;

    public abstract class ScreenBase : MonoBehaviour, IScreen
    {
        protected IScreenManager _screenManager;

        public virtual void Open(IScreenManager manager)
        {
            _screenManager = manager;
        }

        public virtual void Open(IScreenManager manager, params object[] parameters)
        {
            _screenManager = manager;
        }

        public abstract void Close();
    }
}