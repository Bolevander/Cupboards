using UnityEngine;
using UnityEngine.UI;


namespace CupBoards
{
    [RequireComponent(typeof(Image))]
    public class CupBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _image;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _dragColor = new Color(1, 1, 1, 1);

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

        public int Id { get; set; }

        #endregion


        #region UnityMethods

        private void Awake()
        {
            Color temp = new Color(Random.Range(0f, 0.8f), Random.Range(0f, 0.8f), Random.Range(0f, 0.8f));
            _normalColor = temp;
            ImageColor = NormalColor;
            Id = Random.Range(0, 10000);
        }

        protected virtual void OnValidate()
        {
            if (_image == null)
            {
                _image = GetComponent<Image>();
            }
            ImageColor = NormalColor;
        }

        #endregion
    }
}