using CoreSystems.DependencyInjection;
namespace CoreSystems.GridBased2D
{
    using UnityEngine;

    /// <summary>
    /// All Controlls of block selection and target marks
    /// </summary>
    public abstract class SelectableBlock : MonoInstaller, ISelectableBlock
    {
        private Vector2 _gridPosition => transform.localPosition;

        public override void InstallDependency(IInjecter injecter)
        {
            injecter.StoreDependency<ISelectableBlock>(this);
        }

        private void OnMouseDown()
        {
            SelectBlock();
        }

        public abstract void SelectBlock();
    }
}