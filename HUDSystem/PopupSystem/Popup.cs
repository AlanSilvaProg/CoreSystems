using UnityEngine;

namespace CoreSystems.HUDSystem
{
    public abstract partial class Popup : ScreenBase
    {
        [SerializeField] private ScreenPriority _priority;
    }
}

namespace CoreSystems.HUDSystem.Extended
{
    public abstract partial class Popup : ScreenBase
    {
        [SerializeField] private ScreenPriority _priority;
    }
}