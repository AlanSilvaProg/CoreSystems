using UnityEngine;

namespace CoreSystems.HUDSystem
{
    public abstract partial class ScreenBase : MonoBehaviour, IScreen
    {
        protected IScreenManager _screenManager;

        public virtual void Open(IScreenManager manager)
        {
            _screenManager = manager;
        }

        public abstract void Close();
    }
}

namespace CoreSystems.HUDSystem.Extended
{
    public abstract partial class ScreenBase : MonoBehaviour, IScreen
    {
        protected IScreenManager _screenManager;

        public abstract void Close();

        public virtual void Open(IScreenManager manager, params object[] parameters)
        {
            _screenManager = manager;
        }
    }
}