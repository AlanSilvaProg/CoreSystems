using CoreSystems.DependencyInjection;
namespace CoreSystems.GridBased2D
{
    using UnityEngine;

    /// <summary>
    /// All Resources to generate map, including a procedural construction with diferent elements ---- WIP
    /// </summary>
    public class MapGenerator : MonoHybrid, IMapGenerator
    {
        [SerializeField] private MapConfigure _mapConfigure;

        private IMapBlock[][] _mapGrid; // [x][y] 

        //Templates to generate copys to construct the map with
        private IMapBlock _emptyBlock { get { return _mapConfigure.EmptyBlock; } }
        private IMapBlock _holeBlock { get { return _mapConfigure.HoleBlock; } }

        public BlockType GetBlockType(int x, int y) => _mapGrid[x][y].GetBlockType(); // acess the block type with grid position reference 

        public Vector2 GetGlobalPositionOfBlock(Vector2 gridPosition) => _mapGrid[(int)gridPosition.x][(int)gridPosition.y].GetGlobalPosition();

        //Hole generator controll
        private int _maxHoleBlocks;
        private int _holeCount;

        override public void InstallDependency(IInjecter injecter)
        {
            injecter.StoreDependency<IMapGenerator>(this);
        }

        private void Start()
        {
            _maxHoleBlocks = _mapConfigure.GetMaxHoleBlocks(); // getting the max of hole that's possible to coexist in the same map
        }

        [ContextMenu("GenerateMap")]
        public void GenerateMapMatriz()
        {
            _mapGrid = new MapBlock[(int)MapLenght().x][];
            for (int i = 0; i < _mapGrid.Length; i++)
            {
                _mapGrid[i] = new MapBlock[(int)MapLenght().y];
            }
            for (int i = 0; i < _mapGrid.Length; i++)
            {
                for (int y = 0; y < _mapGrid[i].Length; y++)
                {
                    var type = GetNextType();
                    var block = type == BlockType.NORMAL ? _emptyBlock : _holeBlock;
                    _mapGrid[i][y] = block;
                    block.SetupBlock(type, new Vector2(i, y), transform, type == BlockType.HOLE);
                }
            }
        }

        /// <summary>
        /// Get the next type based on the probability to be different 
        /// </summary>
        /// <returns></returns>
        public BlockType GetNextType()
        {
            if (_holeCount < _maxHoleBlocks)
            {
                var redutorIndice = _mapConfigure.GetRedutorHoleIndice();
                var diceBlock = Random.Range(0, redutorIndice) % redutorIndice == 0;
                if (diceBlock)
                {
                    _holeCount++;
                    return BlockType.HOLE;
                }
            }
            return BlockType.NORMAL;
        }

        public Vector2 MapLenght() => _mapConfigure.MapLenght;
    }
}