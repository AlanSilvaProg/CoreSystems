namespace CoreSystems.HUDSystem
{
    using UnityEngine;

    public abstract class PopupManager : MonoBehaviour, IPopupManager
    {
        public abstract void OpenScreen<T>() where T : IScreen, IPopup;

        public abstract void OpenScreen<T>(params object[] parameters) where T : IScreen, IPopup;

        public abstract void CloseScreen(IScreen screen = null);
    }
}