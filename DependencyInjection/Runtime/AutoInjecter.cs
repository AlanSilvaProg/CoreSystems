namespace CoreSystems.DependencyInjection
{
    using UnityEngine;
    public class AutoInjecter : MonoBehaviour
    {
        [SerializeField] private IInject _injectComponent;

        private void Reset()
        {
            _injectComponent = GetComponent<IInject>();
        }

        private void Start()
        {
            _injectComponent.GetDependency();
        }
    }
}