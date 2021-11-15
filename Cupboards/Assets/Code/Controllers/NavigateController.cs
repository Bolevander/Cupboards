using System.Collections.Generic;
using UnityEngine;


namespace CupBoards
{
    public sealed class NavigateController : IExecuteController, IInitializeController
    {
        #region Fields

        private const float _interpolationFramesCount = 30f;
        private readonly GameContext _context;

        private bool _isMoving;
        private float _elapsedFrames;
        private Vector3 _endPointDirection;
        private Vector3 _startPointDirection;
        private CupBehaviour _currentCup;
        private PointBehaviour _previousPoint;
        private PointBehaviour[] _shortestPath;
        private int _pathIndex;
        private List<PointBehaviour[]> _possiblePaths;

        #endregion


        #region ClassLifeCycles

        public NavigateController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            _context.LoadMenu.OnStart += SetOnClickEvent;
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            if (_isMoving == true)
            {
                MoveCup();
            }
        }

        #endregion


        #region Methods

        private void SetOnClickEvent()
        {
            if (_context.Points != null && _context.Points.Count > 0)
            {
                foreach (var cup in _context.Points)
                {
                    cup.OnPointerClickEvent += Click;
                }
            }
        }

        private void Click(PointBehaviour point)
        {
            if (_isMoving == false)
            {
                if (point.placedCup != null)
                {
                    SelectCup(point);
                }
                else
                {
                    StartPath(point);
                }
            }
        }

        private void SelectCup(PointBehaviour point)
        {
            if (_currentCup != null)
            {
                _currentCup.ImageColor = _currentCup.NormalColor;
            }
            foreach (var element in _context.Points)
            {
                HighlightPoint(element, false);
            }

            _currentCup = point.placedCup;
            _previousPoint = point;
            _currentCup.ImageColor = _currentCup.DragColor;

            CreatePaths(point);
        }

        private void StartPath(PointBehaviour point)
        {
            _possiblePaths = new List<PointBehaviour[]>();
            CreatePaths(_previousPoint, point);
            if (_possiblePaths.Count > 0)
            {
                foreach (var element in _context.Points)
                {
                    HighlightPoint(element, false);
                }

                var temp = _possiblePaths[0];
                foreach (var path in _possiblePaths)
                {
                    if (path.Length < temp.Length)
                    {
                        temp = path;
                    }
                }
                _shortestPath = temp;
                SendCupToPoint(_shortestPath[_pathIndex]);
            }
        }

        private void CreatePaths(PointBehaviour currentPoint, PointBehaviour endPoint = null,
            LinkedList<PointBehaviour> pointsMap = null)
        {
            if (pointsMap == null)
            {
                pointsMap = new LinkedList<PointBehaviour>();
            }

            foreach (var point in currentPoint.AvailableTransitions)
            {
                if (point.placedCup == null)
                {
                    if (endPoint == null)
                    {
                        HighlightPoint(point, true);
                    }

                    while (pointsMap.Count > 0 && pointsMap.Last.Value.AvailableTransitions.Contains(point) == false)
                    {
                        pointsMap.RemoveLast();
                    }

                    if (point == endPoint)
                    {
                        pointsMap.AddLast(point);
                        var temp = new PointBehaviour[pointsMap.Count];
                        pointsMap.CopyTo(temp, 0);
                        _possiblePaths.Add(temp);
                        break;
                    }
                    else if (pointsMap.Contains(point) == false)
                    {
                        pointsMap.AddLast(point);
                        CreatePaths(point, endPoint, pointsMap);
                    }
                }
            }
        }

        private void HighlightPoint(PointBehaviour point, bool flag)
        {
            if (flag)
            {
                point.ImageColor = point.HighlightColor;
            }
            else
            {
                point.ImageColor = point.NormalColor;
            }
        }

        private void SendCupToPoint(PointBehaviour point)
        {
            if (_currentCup != null)
            {
                _previousPoint.placedCup = null;
                _previousPoint = point;
                point.placedCup = _currentCup;
                _startPointDirection = _currentCup.transform.position;
                _endPointDirection = point.transform.position;
                _isMoving = true;
            }
        }

        private void MoveCup()
        {
            float interpolationRatio = _elapsedFrames / _interpolationFramesCount;
            _currentCup.transform.position = Vector3.Lerp(_startPointDirection, _endPointDirection, interpolationRatio);
            _elapsedFrames = (_elapsedFrames + 1) % (_interpolationFramesCount + 1);

            if (_currentCup.transform.position == _endPointDirection)
            {
                StopMoving();
            }
        }

        private void StopMoving()
        {
            _elapsedFrames = 0f;            
            if (_pathIndex < _shortestPath.Length - 1)
            {
                _pathIndex++;
                SendCupToPoint(_shortestPath[_pathIndex]);
            }
            else
            {
                _pathIndex = 0;
                _currentCup.ImageColor = _currentCup.NormalColor;
                _currentCup = null;
                _isMoving = false;
                _context.CurrentLevel.OnMove.Invoke();
            }
        }

        #endregion
    }
}
