namespace CoreSystems.HUDSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using CoreSystems.ServiceLocator;

    public abstract partial class ScreenManagerBase : MonoBehaviour, IScreenManager
    {
        protected List<IScreen> _openedScreen = new List<IScreen>();

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
            PublicServiceLocator.s_serviceLocator.RegisterService<IScreenManager>(this);
        }

        public abstract void OpenScreen<T>() where T : IScreen, IPopup;

        public abstract void OpenScreen<T>(params object[] parameters) where T : IScreen, IPopup;

        public virtual void CloseScreen(IScreen screen = null)
        {
            var screenToClose = screen != null ? screen : _openedScreen.Count > 0 ? _openedScreen[0] : null;
            _openedScreen.Remove(screenToClose);
            screenToClose?.Close();
        }
    }
}