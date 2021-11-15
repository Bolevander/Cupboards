using System.Collections.Generic;
using UnityEngine;


namespace CupBoards
{
    public class GameStageController : IInitializeController
    {
        #region Fields

        private readonly GameContext _context;
        private List<int> _cupIds;
        private FinishScreenBehaviour _finishScreen;

        #endregion


        #region ClassLifeCycles

        public GameStageController(GameContext context)
        {
            _context = context;
        }

        #endregion


        #region IInitializeController

        public void Initialize()
        {
            _cupIds = new List<int>();
            _finishScreen = Object.FindObjectOfType<FinishScreenBehaviour>(true);
            _context.LoadMenu.OnStart += SetIds;
        }

        #endregion


        #region Methods

        private void SetIds()
        {
            foreach (var index in _context.CurrentLevel.startCupPoints)
            {
                _cupIds.Add(_context.Points[index - 1].placedCup.Id);
            }

            _context.CurrentLevel.OnMove += CheckFinish;
        }

        private void CheckFinish()
        {
            for (int i = 0; i < _context.CurrentLevel.finishCupPoints.Length; i++)
            {
                if (_context.Points[_context.CurrentLevel.finishCupPoints[i] - 1].placedCup?.Id != _cupIds[i])
                {
                    return;
                }
            }
            Finish();
        }

        private void Finish()
        {
            _finishScreen.gameObject.SetActive(true);
        } 

        #endregion
    }
}
