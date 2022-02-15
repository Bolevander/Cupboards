using UnityEngine;
using UnityEngine.UI;


namespace CupBoards
{
<<<<<<< Updated upstream
    [RequireComponent(typeof(Image))]
=======
>>>>>>> Stashed changes
    public class CupBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _image;
<<<<<<< Updated upstream
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _dragColor = new Color(1, 1, 1, 1);
=======
        [SerializeField] private Color _normalColor = new Color(0.5f, 0.5f, 1, 1);
        [SerializeField] private Color _dragColor = new Color(1, 0.5f, 0.5f, 1);
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
        public int Id { get; set; }

=======
>>>>>>> Stashed changes
        #endregion


        #region UnityMethods

<<<<<<< Updated upstream
        private void Awake()
        {
            Color temp = new Color(Random.Range(0f, 0.8f), Random.Range(0f, 0.8f), Random.Range(0f, 0.8f));
            _normalColor = temp;
            ImageColor = NormalColor;
            Id = Random.Range(0, 10000);
        }

=======
>>>>>>> Stashed changes
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
    }
}