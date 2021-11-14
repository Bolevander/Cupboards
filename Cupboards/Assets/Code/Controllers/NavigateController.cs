using System.Collections.Generic;
using UnityEngine;


namespace CupBoards
{
    public sealed class NavigateController : IExecuteController, IInitializeController
    {
        #region Fields

        private const float _interpolationFramesCount = 30f;

        private bool IsMoving;
        private float _elapsedFrames;
        private Vector3 _endPointDirection;
        private Vector3 _startPointDirection;
        private GameContext _context;
        private List<PointBehaviour> _pointBehaviours = new List<PointBehaviour>();

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
            _pointBehaviours.AddRange(Object.FindObjectsOfType<PointBehaviour>(true));
            if (_pointBehaviours != null && _pointBehaviours.Count > 0)
            {
                foreach (var cup in _pointBehaviours)
                {
                    cup.OnPointerClickEvent += Click;
                }
            }
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            if (IsMoving)
            {
                float interpolationRatio = _elapsedFrames / _interpolationFramesCount;
                _context.CurrentCup.transform.position = Vector3.Lerp(_startPointDirection, _endPointDirection, interpolationRatio);
                _elapsedFrames = (_elapsedFrames + 1) % (_interpolationFramesCount + 1);

                if (_context.CurrentCup.transform.position == _endPointDirection)
                {
                    StopMoving();
                }
            }
        }

        #endregion


        #region Methods

        private void Click(PointBehaviour point)
        {
            if (IsMoving == false)
            {
                if (point.placedCup != null)
                {
                    if (_context.CurrentCup != null)
                    {
                        _context.CurrentCup.ImageColor = _context.CurrentCup.NormalColor;
                    }

                    _context.CurrentCup = point.placedCup;
                    _context.PreviousPoint = point;
                    _context.CurrentCup.ImageColor = _context.CurrentCup.DragColor;
                }
                else
                {
                    if (_context.CurrentCup != null)
                    {
                        _context.PreviousPoint.placedCup = null;
                        point.placedCup = _context.CurrentCup;
                        _startPointDirection = _context.CurrentCup.transform.position;
                        _endPointDirection = point.transform.position;

                        IsMoving = true;
                    }
                }
            }
        }

        private void StopMoving()
        {
            _elapsedFrames = 0f;
            _context.CurrentCup.ImageColor = _context.CurrentCup.NormalColor;
            _context.CurrentCup = null;

            IsMoving = false;
        }

        #endregion
    }
}
