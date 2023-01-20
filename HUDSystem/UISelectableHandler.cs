namespace CoreSystems.HUDSystem
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    public class UISelectableHandler : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        private const float AnimationVelocity = 1;

        [SerializeField] private Selectable _selectable;
        [Range(0.1f, 0.75f)]
        [SerializeField] private float _animationEffect = 0.75f;

        private Vector2 _initialSize;
        private Vector2 _downEffectSize;

        private int _state;
        private Vector2 _velocity;

        private RectTransform _myRectTransform => (RectTransform)transform;

#if UNITY_EDITOR
        #region EDITOR 
    private void Reset() => GetResource();

    private void OnValidate() => GetResource();

    private void GetResource()
    {
        if (!_selectable)
        {
            _selectable = GetComponent<Selectable>();
            _selectable.transition = Selectable.Transition.None;
        }
    }
        #endregion
#endif

        private void Awake()
        {
            _initialSize = _myRectTransform.sizeDelta;
            _downEffectSize = _initialSize * _animationEffect;
            _velocity = Vector2.one * AnimationVelocity;
        }

        private void LateUpdate()
        {
            if (_state == 0)
            {
                if (_myRectTransform.sizeDelta != _initialSize)
                    UpAnimation();
            }
            else
            {
                if (_myRectTransform.sizeDelta != _downEffectSize)
                    DownAnimation();
            }
        }

        private void DownAnimation()
        {
            _myRectTransform.sizeDelta = Vector2.SmoothDamp(_myRectTransform.sizeDelta, _downEffectSize, ref _velocity, Time.deltaTime);
        }

        private void UpAnimation()
        {
            _myRectTransform.sizeDelta = Vector2.SmoothDamp(_myRectTransform.sizeDelta, _initialSize, ref _velocity, Time.deltaTime);
        }

        #region OnPointer's Interface implementation

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_selectable.interactable)
                _state = 1;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_state == 1)
            {
                _state = 0;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_state == 1)
                _state = 0;
        }

        #endregion

    }
}