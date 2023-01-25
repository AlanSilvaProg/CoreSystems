using UnityEngine;

namespace CoreSystems.HUDSystem
{
    public abstract partial class ScreenBase : MonoBehaviour, IScreen
    {
        public abstract void Open();

        public abstract void Close();
    }
}

namespace CoreSystems.HUDSystem.Extended
{
    public abstract partial class ScreenBase : MonoBehaviour, IScreen
    {
        public abstract void Close();

        public abstract void Open(params object[] parameters);
    }
}