using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CupBoards
{
    public class GameStageController : IInitializeController, ITearDownController
    {
        #region Fields

        private readonly GameContext _context;
        private List<int> _cupIds;
        private FinishScreenBehaviour _finishScreen;
        private LevelContainerBehaviour _levelContainer;

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
            _levelContainer = Object.FindObjectOfType<LevelContainerBehaviour>();
            _finishScreen.restartButton.onClick.AddListener(Restart);
            _levelContainer.restartButton.onClick.AddListener(Restart);

            _context.LoadMenu.OnStart += SetIds;
            _context.LoadMenu.OnStart += TurnOnRestartButton;
        }

        #endregion


        #region ICleanupController

        public void TearDown()
        {
            _context.LoadMenu.OnStart -= SetIds;
            _context.LoadMenu.OnStart -= TurnOnRestartButton;
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

        private void TurnOnRestartButton()
        {
            _levelContainer.restartButton.gameObject.SetActive(true);
        }

        private void Finish()
        {
            _finishScreen.gameObject.SetActive(true);
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        #endregion
    }
}
