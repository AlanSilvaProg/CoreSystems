namespace CoreSystems.GridBased2D
{
    using UnityEngine;

    /// <summary>
    /// A Path element is a representation of one block on the map grid to calculate rotes and paths
    /// </summary>
    public class PathElement
    {
        private const int MovementCost = 10;

        public float HCost; // distance 
        public int GCost = 0; // walk cost
        public float FCost { get { return HCost + GCost; } }

        public PathElement ParentElement;

        public IPathResolver PathResolver;

        public Vector2 GridPosition;

        public bool IsTarget;
        public bool Blocked;

        /// <summary>
        /// Initial Setup of this one with core informations present on grid map
        /// </summary>
        /// <param name="position"></param>
        /// <param name="isTarget"></param>
        /// <param name="blocked"></param>
        public PathElement(Vector2 position, bool isTarget, bool blocked)
        {
            GridPosition = position;
            IsTarget = isTarget;
            Blocked = blocked;
        }

        /// <summary>
        /// Calculate based on parent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="positionOnGrid"></param>
        public void Calculate(PathElement parent, Vector2 positionOnGrid)
        {
            ParentElement = parent;
            PathResolver = parent.PathResolver;
            GCost = parent.GCost + MovementCost;
            CalculateDistance();
        }

        private void CalculateDistance()
        {
            if (IsTarget) return;
            HCost = Vector2.Distance(PathResolver.GetTargetPosition(), GridPosition);
        }
    }
}