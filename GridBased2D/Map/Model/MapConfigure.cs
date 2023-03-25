namespace CoreSystems.GridBased2D
{
    using UnityEngine;

    // To create a SO and customize the map generation options, just uncomment the line below and create a new SO
    //[CreateAssetMenu]
    public class MapConfigure : ScriptableObject
    {
        [Tooltip("Vector2(width, height)")]
        [SerializeField] private Vector2 _mapLenght;
        [Header("Templates")]
        [SerializeField] private GameObject _emptyBlockRef;
        [SerializeField] private GameObject _holeBlockRef;

        [Header("Map Generation Settings")]
        [Range(0, 20)]
        [SerializeField] private int _maxNumberOfHoles;
        [Range(15, 99)]
        [SerializeField] private int _redutorOfChanceToHoleAppear;

        private IMapBlock _emptyBlock;
        private IMapBlock _holeBlock;

        public int GetRedutorHoleIndice() => _redutorOfChanceToHoleAppear;
        public Vector2 MapLenght => _mapLenght;
        public IMapBlock EmptyBlock
        {
            get
            {
                if (_emptyBlock == null) _emptyBlock = _emptyBlockRef.GetComponent<IMapBlock>();
                return _emptyBlock.GetOneCopy();
            }
        }
        public IMapBlock HoleBlock
        {
            get
            {
                if (_holeBlock == null) _holeBlock = _holeBlockRef.GetComponent<IMapBlock>();
                return _holeBlock.GetOneCopy();
            }
        }

        public int GetMaxHoleBlocks() => _maxNumberOfHoles;

    }
}