namespace CoreSystems.HUDSystem
{
    using UnityEngine;
    using UnityEngine.UI;

    public abstract class ScreenBase : MonoBehaviour, IScreen
    {
        protected IScreenManager _screenManager;
        [SerializeField] protected Button _defaultConfirmButton;
        [SerializeField] protected Button[] _buttons;

        protected bool _internalCall;

        protected virtual void Awake()
        {
            SetupButtons();
        }

        public virtual void SetupButtons()
        {
            _defaultConfirmButton.onClick.AddListener(CloseCall);
            foreach (var btn in _buttons) btn.onClick.AddListener(CloseCall);

            void CloseCall()
            {
                _internalCall = true;
                Close();
            }
        }

        public virtual void Open(IScreenManager manager)
        {
            _screenManager = manager;
        }

        public virtual void Open(IScreenManager manager, params object[] parameters)
        {
            _screenManager = manager;
        }

        public abstract void Close();

        public virtual void ChangeButtonState(bool state)
        {
            _defaultConfirmButton.interactable = state;
            foreach (Button btn in _buttons) btn.interactable = state;
        }
    }
}