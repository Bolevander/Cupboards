using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace CupBoards
{
    public class PointBehaviour : MonoBehaviour, IPointerClickHandler
    {
        public CupBehaviour placedCup;
        public event Action<PointBehaviour> OnPointerClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickEvent?.Invoke(this);
        }
    }
}
