using UnityEngine;


namespace CupBoards
{
    public class LevelController : IInitializeController
    {
        #region Fileds

        public const int UnityUnitSize = 100;

        private LevelData[] _levels;
        private Canvas _canvas;
        private CupBehaviour _cup;
        private LineRenderer _pathLine;
        private PointBehaviour _point;

        #endregion


        public void Initialize()
        {
            _levels = LevelLoader.LoadAllSaves();
            _canvas = Object.FindObjectOfType<Canvas>();

            var path = AssetsPathGameObject.Object[GameObjectType.Cup];
            _cup = Resources.Load<CupBehaviour>(path);
            path = AssetsPathGameObject.Object[GameObjectType.PathLine];
            _pathLine = Resources.Load<LineRenderer>(path);
            path = AssetsPathGameObject.Object[GameObjectType.Point];
            _point = Resources.Load<PointBehaviour>(path);


            LoadLevel(_levels[0]);//for test purposes
        }

        public void LoadLevel(LevelData level)
        {
            for (int i = 0; i < level.pointsCoordinates.Length; i++)
            {
                InstantiateObject(_point, level.pointsCoordinates[i]);
            }

            for (int i = 0; i < level.startCupPoints.Length; i++)
            {
                InstantiateObject(_cup, level.pointsCoordinates[level.startCupPoints[i] - 1]);
            }

            LineRenderer lineInstance;
            for (int i = 0; i < level.linksBetweenPoints.Length; i++)
            {
                lineInstance = InstantiateObject(_pathLine, Vector2.zero);
                lineInstance.SetPosition(1, level.pointsCoordinates[level.linksBetweenPoints[i].Item1 - 1] / UnityUnitSize);
                lineInstance.SetPosition(0, level.pointsCoordinates[level.linksBetweenPoints[i].Item2 - 1] / UnityUnitSize);
            }
        }

        private T InstantiateObject<T>(T prefab, Vector2 coords) where T : Component
        {
            var halfHeight = Screen.currentResolution.height / 2;
            var halfWidth = Screen.currentResolution.width / 2;
            T instance = Object.Instantiate(prefab, new Vector3(coords.x + halfWidth, coords.y + halfHeight),
                _canvas.transform.rotation, _canvas.transform);
            instance.transform.localScale = new Vector3(1 / _canvas.transform.localScale.x,
                1 / _canvas.transform.localScale.y);

            return instance;
        }
    }
}
