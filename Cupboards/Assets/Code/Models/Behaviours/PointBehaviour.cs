using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace CupBoards
{
<<<<<<< Updated upstream
    [RequireComponent(typeof(Image))]
=======
>>>>>>> Stashed changes
    public class PointBehaviour : MonoBehaviour, IPointerClickHandler
    {
        #region Fields

        public event Action<PointBehaviour> OnPointerClickEvent;

        public CupBehaviour placedCup;

        [SerializeField] private Image _image;
<<<<<<< Updated upstream
        [SerializeField] private Color _normalColor = new Color(0.8f, 0.8f, 0.8f, 1);
        [SerializeField] private Color _highlightColor = new Color(0.6f, 0.9f, 0.6f, 1);
=======
        [SerializeField] private Color _normalColor = new Color(0.6f, 0.6f, 0.6f, 1);
        [SerializeField] private Color _highlightColor = new Color(0.6f, 1, 0.6f, 1);
>>>>>>> Stashed changes

        #endregion


        #region Properties

        public List<PointBehaviour> AvailableTransitions { get; set; }
        public Color NormalColor => _normalColor;
        public Color HighlightColor => _highlightColor;
        public Color ImageColor
        {
            get => _image.color;
            set
            {
                if (value == _normalColor || value == _highlightColor)
                {
                    _image.color = value;
                }
                else
                {
                    Debug.Log("Invalid color");
                }
            }
        }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            AvailableTransitions = new List<PointBehaviour>();
        }

        protected virtual void OnValidate()
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
<<<<<<< Updated upstream
            ImageColor = NormalColor;
=======
            ImageColor = _normalColor;
>>>>>>> Stashed changes
        }

        #endregion


        #region Methods

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickEvent?.Invoke(this);
        }

        #endregion
    }
}
