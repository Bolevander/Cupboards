using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CupBoards
{
    public sealed class LevelController : IInitializeController
    {
        #region Fields

        private const float LINE_HEIGHT = 30;
        private readonly GameContext _context;

        private LevelContainerBehaviour _levelContainer;
        private CupBehaviour _cup;
        private RectTransform _pathLine;
        private PointBehaviour _point;
        private Button _loadButton;
        private GameObject _buttonContainer;

        #endregion


        #region ClassLifeCycles

        public LevelController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            _context.Levels = new List<LevelData>();
            _context.Levels.AddRange(LevelLoader.LoadAllSaves());
            _context.Points = new List<PointBehaviour>();
            _context.LoadMenu = Object.FindObjectOfType<LoadMenuBehaviour>(true);
            _buttonContainer = _context.LoadMenu.buttonsContainer;
            _levelContainer = Object.FindObjectOfType<LevelContainerBehaviour>();
            _context.CupSpeed = _levelContainer.cupSpeed;

            var path = AssetsPathGameObject.Object[GameObjectType.Cup];
            _cup = Resources.Load<CupBehaviour>(path);
            path = AssetsPathGameObject.Object[GameObjectType.PathLine];
            _pathLine = Resources.Load<RectTransform>(path);
            path = AssetsPathGameObject.Object[GameObjectType.Point];
            _point = Resources.Load<PointBehaviour>(path);
            path = AssetsPathGameObject.Object[GameObjectType.LoadButton];
            _loadButton = Resources.Load<Button>(path);

            InstantiateLoadButtons(_buttonContainer);
            _context.LoadMenu.gameObject.SetActive(true);
        }

        #endregion


        #region Methods

        private void CreateLevel()
        {
            InstantiateLines(_context.CurrentLevel);
            InstantiatePoints(_context.CurrentLevel);
            InstantiateCups(_context.CurrentLevel);
            SetAvailableTransitioins(_context.CurrentLevel);
            _context.LoadMenu.OnStart.Invoke();
        }

        private void InstantiateLoadButtons(GameObject parent)
        {
            for (int i = 0; i < _context.Levels.Count; i++)
            {
                var button = Object.Instantiate(_loadButton, parent.transform);
                button.GetComponentInChildren<Text>().text = $"Уровень {i + 1}";
                var i1 = i;
                button.onClick.AddListener(() => _context.CurrentLevel = _context.Levels[i1]);
                button.onClick.AddListener(CreateLevel);
                button.onClick.AddListener(_context.LoadMenu.Hide);
            }
        }

        private void InstantiateLines(LevelData level)
        {
            var rows = level.AdjacencyMatrix.GetUpperBound(0) + 1;
            var columns = level.AdjacencyMatrix.Length / rows;

            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < columns; j++)
                {
                    if (level.AdjacencyMatrix[i, j] == 1)
                    {
                        var lineInstance = InstantiateObject(_pathLine, level.pointsCoordinates[i - 1]);
                        lineInstance.rotation = GetLineRotation(level, i - 1, j - 1);
                        lineInstance.sizeDelta = new Vector2(GetLineWidth(level, i - 1, j - 1), LINE_HEIGHT);
                    }
                }
            }
        }

        private void InstantiatePoints(LevelData level)
        {
            for (int i = 0; i < level.pointsCoordinates.Length; i++)
            {
                var pointInstance = InstantiateObject(_point, level.pointsCoordinates[i]);
                _context.Points.Add(pointInstance);
            }
        }

        private void InstantiateCups(LevelData level)
        {
            for (int i = 0; i < level.startCupPoints.Length; i++)
            {
                var cupInstance = InstantiateObject(_cup, level.pointsCoordinates[level.startCupPoints[i] - 1]);
                _context.Points[level.startCupPoints[i] - 1].placedCup = cupInstance;
            }
        }

        private void SetAvailableTransitioins(LevelData level)
        {
            var length = level.AdjacencyMatrix.GetUpperBound(1) + 1;
            for (int i = 0; i < _context.Points.Count; i++)
            {
                for (int j = 1; j < length; j++)
                {
                    if (level.AdjacencyMatrix[i + 1, j] == 1)
                    {
                        _context.Points[i].AvailableTransitions.Add(_context.Points[j - 1]);
                        _context.Points[j - 1].AvailableTransitions.Add(_context.Points[i]);
                    }
                }
            }
        }

        private T InstantiateObject<T>(T prefab, Vector2 coords) where T : Component
        {
            var halfHeight = Screen.currentResolution.height / 2;
            var halfWidth = Screen.currentResolution.width / 2;
            T instance = Object.Instantiate(prefab, new Vector3(coords.x + halfWidth, coords.y + halfHeight),
                _levelContainer.transform.rotation, _levelContainer.transform);
            instance.transform.localScale = new Vector3(1 / _levelContainer.transform.localScale.x,
                1 / _levelContainer.transform.localScale.y);

            return instance;
        }

        private Quaternion GetLineRotation(LevelData level, int index1, int index2)
        {
            var dirrection = level.pointsCoordinates[index1] - level.pointsCoordinates[index2];
            var angle = Vector3.Angle(Vector3.left, dirrection);
            var axis = Vector3.Cross(Vector3.left, dirrection);

            return Quaternion.AngleAxis(angle, axis);
        }

        private float GetLineWidth(LevelData level, int index1, int index2)
        {
            return (level.pointsCoordinates[index2] - level.pointsCoordinates[index1]).magnitude;
        }
    }

    #endregion
}