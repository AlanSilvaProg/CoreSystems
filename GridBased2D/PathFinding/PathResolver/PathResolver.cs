using CoreSystems.DependencyInjection;
namespace CoreSystems.GridBased2D
{
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// Resolver with Pathfinding Algorithm A* without vertical moves
    /// </summary>
    public class PathResolver : MonoHybrid, IPathResolver
    {
        public override void InstallDependency(IInjecter injecter)
        {
            injecter.StoreDependency<IPathResolver>(this);
        }

        [Inject] private IMapGenerator _mapGenerator;

        private PathElement[][] _pathElements;

        private List<PathElement> _openList = new List<PathElement>();
        private List<PathElement> _closeList = new List<PathElement>();

        private List<Vector2> _correctWay = new List<Vector2>();

        private Vector2 _gridLenght;
        private Vector2 _initialPoint;
        private Vector2 _targetPoint;

        private bool _targetReached = false;

        /// <summary>
        /// Generate a correct and better way to reach one target point on map grid
        /// </summary>
        /// <param name="initialPoint"></param>
        /// <param name="targetPoint"></param>
        /// <param name="gridLenght"></param>
        /// <returns></returns>
        public Vector2[] GetNewPathWay(Vector2 initialPoint, Vector2 targetPoint, Vector2 gridLenght)
        {
            _initialPoint = initialPoint;
            _targetPoint = targetPoint;
            _gridLenght = gridLenght;

            ResetConfigurations();
            SetupPathElements();

            _openList.Add(_pathElements[(int)_initialPoint.x][(int)_initialPoint.y]);
            var currentIndex = 0;
            _openList[currentIndex].PathResolver = this;
            CalculateNeighbors(currentIndex);
            currentIndex = SelectNextPoint();

            while (!_targetReached)
            {
                CalculateNeighbors(currentIndex);
                currentIndex = SelectNextPoint();
            }

            SetupCorrectWay();

            return _correctWay.ToArray();
        }

        private void CalculateNeighbors(int index)
        {
            var aroundPos = _openList[index].GridPosition;
            var aroundElement = _openList[index];

            bool xNegativePermission = aroundPos.x - 1 >= 0 && !PresentOnLists(aroundPos + Vector2.left);
            bool xPositivePermission = aroundPos.x + 1 < _gridLenght.x && !PresentOnLists(aroundPos + Vector2.right);
            bool yNegativePermission = aroundPos.y - 1 >= 0 && !PresentOnLists(aroundPos + Vector2.down);
            bool yPosivePermission = aroundPos.y + 1 < _gridLenght.y && !PresentOnLists(aroundPos + Vector2.up);

            if (xNegativePermission)
            {
                var element = _pathElements[(int)aroundPos.x - 1][(int)aroundPos.y];
                if (!element.Blocked)
                {
                    element.Calculate(aroundElement, aroundElement.GridPosition + Vector2.left);
                    _openList.Add(element);
                }
            }
            if (xPositivePermission)
            {
                var element = _pathElements[(int)aroundPos.x + 1][(int)aroundPos.y];
                if (!element.Blocked)
                {
                    element.Calculate(aroundElement, aroundElement.GridPosition + Vector2.right);
                    _openList.Add(element);
                }
            }
            if (yNegativePermission)
            {
                var element = _pathElements[(int)aroundPos.x][(int)aroundPos.y - 1];
                if (!element.Blocked)
                {
                    element.Calculate(aroundElement, aroundElement.GridPosition + Vector2.down);
                    _openList.Add(element);
                }
            }
            if (yPosivePermission)
            {
                var element = _pathElements[(int)aroundPos.x][(int)aroundPos.y + 1];
                if (!element.Blocked)
                {
                    element.Calculate(aroundElement, aroundElement.GridPosition + Vector2.up);
                    _openList.Add(element);
                }
            }

            _openList.Remove(aroundElement);
            _closeList.Add(aroundElement);
        }

        /// <summary>
        /// Check if this element exist in any list ( if exist, has been calculated and dont need to calculate again )
        /// </summary>
        /// <param name="aroundPos"></param>
        /// <returns></returns>
        private bool PresentOnLists(Vector2 aroundPos)
        {
            return _openList.Contains(_pathElements[(int)aroundPos.x][(int)aroundPos.y]) || _closeList.Contains(_pathElements[(int)aroundPos.x][(int)aroundPos.y]);
        }

        /// <summary>
        /// Get on open list the better element to make the next sum
        /// </summary>
        /// <returns></returns>
        private int SelectNextPoint()
        {
            if (!_targetReached)
            {
                var lowestDistance = 0;
                for (int i = 1; i < _openList.Count; i++)
                {
                    if (_openList[i].HCost < _openList[lowestDistance].HCost)
                        lowestDistance = i;
                }
                if (_openList[lowestDistance].IsTarget)
                {
                    _targetReached = true;
                    var element = _openList[lowestDistance];
                    _closeList.Add(element);
                    _openList.Remove(element);
                }
                return lowestDistance;
            }

            return 0;
        }

        public Vector2 GetTargetPosition() => _targetPoint;

        /// <summary>
        /// Getting the correct way and receiving a reverse value of him 
        /// </summary>
        private void SetupCorrectWay()
        {
            var currentPath = _closeList[_closeList.Count - 1];
            _correctWay.Add(_mapGenerator.GetGlobalPositionOfBlock(currentPath.GridPosition));
            while (currentPath.ParentElement != null)
            {
                currentPath = currentPath.ParentElement;
                _correctWay.Add(_mapGenerator.GetGlobalPositionOfBlock(currentPath.GridPosition));
            }
            _correctWay.Reverse();

        }

        /// <summary>
        /// Initial setup to get all PathElements of one map grid
        /// </summary>
        private void SetupPathElements()
        {
            _pathElements = new PathElement[(int)_gridLenght.x][];
            for (int i = 0; i < _gridLenght.x; i++)
            {
                _pathElements[i] = new PathElement[(int)_gridLenght.y];
                for (int y = 0; y < _gridLenght.y; y++)
                {
                    _pathElements[i][y] = new PathElement(new Vector2(i, y), CheckIfIsTarget(i, y), _mapGenerator.GetBlockType(i, y) == BlockType.HOLE);
                }
            }
        }

        private void ResetConfigurations()
        {
            _openList.Clear();
            _closeList.Clear();
            _correctWay.Clear();
            _targetReached = false;
        }

        /// <summary>
        /// Checking if the target has been reached
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool CheckIfIsTarget(int x, int y) => x == _targetPoint.x && y == _targetPoint.y;
    }
}