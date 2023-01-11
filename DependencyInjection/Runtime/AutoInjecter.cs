namespace CoreSystems.DependencyInjection
{
    using UnityEngine;
    public class AutoInjecter : MonoBehaviour
    {
        [SerializeField] private IInject _injectComponent;

        private void Start()
        {
            _injectComponent = GetComponent<IInject>();
            _injectComponent.GetDependency();
        }
    }
}