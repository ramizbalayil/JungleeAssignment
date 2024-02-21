using UnityEngine;
using UnityEngine.UI;

namespace junglee.cards
{
    public class SingleCardHolder : MonoBehaviour
    {
        [SerializeField] private Image _cardUI;

        private RectTransform _rectT;
        private RectTransform _cardRectT;

        public float Width => _rectT.sizeDelta.x;

        public float CardWidth => _cardRectT.sizeDelta.x;

        private void Awake()
        {
            _rectT = transform as RectTransform;
            _cardRectT = _cardUI.transform as RectTransform;
        }

        public void SetCard(Sprite tex)
        {
            _cardUI.sprite = tex;
        }
    }
}