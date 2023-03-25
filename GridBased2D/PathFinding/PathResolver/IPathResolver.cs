namespace CoreSystems.GridBased2D
{
    using UnityEngine;

    public interface IPathResolver
    {
        Vector2[] GetNewPathWay(Vector2 initialPoint, Vector2 targetPoint, Vector2 gridLenght);
        Vector2 GetTargetPosition();
    }
}