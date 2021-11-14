using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace CupBoards
{
    public class PointBehaviour : MonoBehaviour, IPointerClickHandler
    {
        #region Fields

        public CupBehaviour placedCup;
        public event Action<PointBehaviour> OnPointerClickEvent;

        #endregion


        #region Methods

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickEvent?.Invoke(this);
        }

        #endregion
    }
}
