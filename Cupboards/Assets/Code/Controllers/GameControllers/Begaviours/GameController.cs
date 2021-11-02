using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace CupBoards
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields

        private GameStateController _activeController;

        #endregion


        #region UnityMethods

        private void Start()
        {
            GameContext context = new GameContext();

            _activeController = new GameSystemsController(context);
            _activeController.Initialize();

            Time.timeScale = 1;
        }

        private void FixedUpdate()
        {
            _activeController.Execute(UpdateType.Fixed);
            _activeController.Cleanup(UpdateType.Fixed);
        }

        private void Update()
        {
            _activeController.Execute(UpdateType.Update);
            _activeController.Cleanup(UpdateType.Update);
        }

        private void LateUpdate()
        {
            _activeController.Execute(UpdateType.Late);
            _activeController.Cleanup(UpdateType.Update);
        }

        private void OnDestroy()
        {
            _activeController.TearDown();
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (!EditorApplication.isPlaying)
            {
                return;
            }
            _activeController.Execute(UpdateType.Gizmos);
            _activeController.Cleanup(UpdateType.Gizmos);
        }

#endif

        #endregion
    }
}
