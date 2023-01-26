namespace CoreSystems.HUDSystem
{
    using UnityEngine;

    public abstract partial class Popup : MonoBehaviour, IPopup
    {
        public abstract void Open(IScreenManager manager);

        public abstract void Open(IScreenManager manager, params object[] parameters);

        public abstract void Close();
    }
}