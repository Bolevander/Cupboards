using System.Collections.Generic;
using UnityEngine;


namespace CupBoards
{
    public sealed class LevelController : IInitializeController
    {
        #region Fileds

        private const float LINE_HEIGHT = 30;

        private LevelData[] _levels;
        private List<PointBehaviour> _points;

        private Canvas _canvas;
        private CupBehaviour _cup;
        private RectTransform _pathLine;
        private PointBehaviour _point;

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            _levels = LevelLoader.LoadAllSaves();
            _points = new List<PointBehaviour>();
            _canvas = Object.FindObjectOfType<Canvas>();

            var path = AssetsPathGameObject.Object[GameObjectType.Cup];
            _cup = Resources.Load<CupBehaviour>(path);
            path = AssetsPathGameObject.Object[GameObjectType.PathLine];
            _pathLine = Resources.Load<RectTransform>(path);
            path = AssetsPathGameObject.Object[GameObjectType.Point];
            _point = Resources.Load<PointBehaviour>(path);

            LoadLevel(_levels[0]);//for test purposes
        }

        #endregion


        #region Methods

        public void LoadLevel(LevelData level)
        {
            for (int i = 0; i < level.linksBetweenPoints.Length; i++)
            {
                var lineInstance = InstantiateObject(_pathLine, level.pointsCoordinates[level.linksBetweenPoints[i].
                    Item1 - 1]);
                lineInstance.rotation = GetLineRotation(level, i);
                lineInstance.sizeDelta = new Vector2(GetLineWidth(level, i), LINE_HEIGHT);
            }

            for (int i = 0; i < level.pointsCoordinates.Length; i++)
            {
                _points.Add(InstantiateObject(_point, level.pointsCoordinates[i]));
            }

            for (int i = 0; i < level.startCupPoints.Length; i++)
            {
                var cupInstance = InstantiateObject(_cup, level.pointsCoordinates[level.startCupPoints[i] - 1]);
                _points[level.startCupPoints[i] - 1].placedCup = cupInstance;
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

        private Quaternion GetLineRotation(LevelData level, int index)
        {
            var dirrection = level.pointsCoordinates[level.linksBetweenPoints[index].Item1 - 1] -
                    level.pointsCoordinates[level.linksBetweenPoints[index].Item2 - 1];
            var angle = Vector3.Angle(Vector3.left, dirrection);
            var axis = Vector3.Cross(Vector3.left, dirrection);

            return Quaternion.AngleAxis(angle, axis);
        }

        private float GetLineWidth(LevelData level, int index)
        {
            return (level.pointsCoordinates[level.linksBetweenPoints[index].Item2 - 1]
                    - level.pointsCoordinates[level.linksBetweenPoints[index].Item1 - 1]).magnitude;
        }
    } 

    #endregion
}