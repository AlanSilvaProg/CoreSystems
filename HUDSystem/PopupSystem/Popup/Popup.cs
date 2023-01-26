namespace CoreSystems.HUDSystem
{
    using UnityEngine;
    using UnityEngine.UI;

    public abstract class Popup : MonoBehaviour, IPopup
    {
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

        public abstract void Open(IScreenManager manager);

        public abstract void Open(IScreenManager manager, params object[] parameters);

        public abstract void Close();

        public virtual void ChangeButtonState(bool state)
        {
            _defaultConfirmButton.interactable = state;
            foreach (Button btn in _buttons) btn.interactable = state;
        }
    }
}