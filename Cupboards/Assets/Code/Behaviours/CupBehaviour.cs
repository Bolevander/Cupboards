using UnityEngine;


namespace CupBoards
{
    public class CupBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Color _normalColor = new Color(0.5f, 0.5f, 1, 1);
        [SerializeField] private Color _dragColor = new Color(1, 0.5f, 0.5f, 1);

        #endregion


        #region Properties

        public Color NormalColor => _normalColor;
        public Color DragColor => _dragColor;
        public Color ImageColor
        {
            get => _sprite.color;
            set
            {
                if (value == _normalColor || value == _dragColor)
                {
                    _sprite.color = value;
                }
                else
                {
                    Debug.Log("Недопустимый цвет");
                }
            }
        }

        #endregion


        #region UnityMethods

        protected virtual void OnValidate()
        {
            if (_sprite == null)
            {
                _sprite = GetComponent<SpriteRenderer>();
            }
            ImageColor = _normalColor;
        }

        #endregion
    }
}