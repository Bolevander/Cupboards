using UnityEngine;
using UnityEngine.UI;


namespace CupBoards
{
    public class LevelContainerBehaviour : MonoBehaviour
    {
        #region Fields

        [Range(1, 10)]
        public int cupSpeed = 1;
        public Button restartButton; 

        #endregion
    }
}
