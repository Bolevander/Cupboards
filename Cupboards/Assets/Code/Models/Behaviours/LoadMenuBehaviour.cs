using System;
using UnityEngine;


namespace CupBoards
{
    public class LoadMenuBehaviour : MonoBehaviour
    {
        #region Fields

<<<<<<< Updated upstream
        public Action OnStart = () => { };
=======
        public Action OnLoad = () => { };
>>>>>>> Stashed changes
        public GameObject buttonsContainer;

        #endregion


        #region Methods

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}
