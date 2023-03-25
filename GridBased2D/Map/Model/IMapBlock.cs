namespace CoreSystems.GridBased2D
{
    using UnityEngine;

    public interface IMapBlock
    {
        Vector2 GetGlobalPosition();
        IMapBlock GetOneCopy();
        BlockType GetBlockType();
        void SetupBlock(BlockType type, Vector2 gridPosition, Transform parent, bool isBlocked);
    }
}