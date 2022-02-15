using System;
using UnityEngine;


namespace CupBoards
{
    public class LoadMenuBehaviour : MonoBehaviour
    {
        #region Fields

        public Action OnStart = () => { };
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
