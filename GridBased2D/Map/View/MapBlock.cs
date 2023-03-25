using CoreSystems.DependencyInjection;
namespace CoreSystems.GridBased2D
{
    using UnityEngine;

    //All block resources, correponding a one tile on the grid

    [RequireComponent(typeof(ISelectableBlock))]
    public class MapBlock : MonoInject, IMapBlock
    {
        [SerializeField] private BlockType _blockType;
        private bool _blocked;
        public Vector2 position;

        [Inject] private ISelectableBlock _selectable;

        public BlockType GetBlockType() => _blockType;

        public ISelectableBlock GetSelectable() => _selectable;

        public Vector2 GetGlobalPosition() => transform.position;

        /// <summary>
        /// Setup the block and storage all core informations about him
        /// </summary>
        /// <param name="type"></param>
        /// <param name="gridPosition"></param>
        /// <param name="parent"></param>
        /// <param name="navigator"></param>
        /// <param name="indicator"></param>
        /// <param name="actionSelectionRound"></param>
        /// <param name="isBlocked"></param>
        public void SetupBlock(BlockType type, Vector2 gridPosition, Transform parent, bool isBlocked)
        {
            _blockType = type;
            position = gridPosition;
            transform.SetParent(parent);
            transform.localPosition = gridPosition;
            _blocked = isBlocked;
        }

        /// <summary>
        /// Generate one identical copy of this block and treat like a new one
        /// </summary>
        /// <returns></returns>
        public IMapBlock GetOneCopy()
        {
            return Instantiate(this).GetComponent<IMapBlock>();
        }
    }
}