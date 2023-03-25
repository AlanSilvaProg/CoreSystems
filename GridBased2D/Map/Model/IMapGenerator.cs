namespace CoreSystems.GridBased2D
{
    using UnityEngine;
    public interface IMapGenerator
    {
        BlockType GetBlockType(int x, int y);
        Vector2 GetGlobalPositionOfBlock(Vector2 gridPosition);
        Vector2 MapLenght();
        void GenerateMapMatriz();
        BlockType GetNextType();
    }
}