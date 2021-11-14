using UnityEngine;
using UnityEngine.UI;


namespace CupBoards
{
    public class CupBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _image;
        [SerializeField] private Color _normalColor = new Color(0.5f, 0.5f, 1, 1);
        [SerializeField] private Color _dragColor = new Color(1, 0.5f, 0.5f, 1);

        #endregion


        #region Properties

        public Color NormalColor => _normalColor;
        public Color DragColor => _dragColor;
        public Color ImageColor
        {
            get => _image.color;
            set
            {
                if (value == _normalColor || value == _dragColor)
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

        protected virtual void OnValidate()
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            ImageColor = _normalColor;
        }

        #endregion
    }
}