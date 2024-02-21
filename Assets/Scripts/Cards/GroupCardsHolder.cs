using junglee.config;
using UnityEngine;

namespace junglee.cards
{
    public class GroupCardsHolder : MonoBehaviour
    {
        [SerializeField] private SingleCardHolder _singleCardHolderPrefab;

        private RectTransform _rectT;

        private void Awake()
        {
            _rectT = transform as RectTransform;
        }

        public void SetWidth()
        {
            SingleCardHolder card = transform.GetChild(0).GetComponent<SingleCardHolder>();
            float width = 0f;
            float obj_width = card.Width;

            foreach(Transform child in transform)
            {
                width += obj_width;
            }

            width += (card.CardWidth - obj_width);

            _rectT.sizeDelta = new Vector2(width, _rectT.sizeDelta.y);
        }

        public void AddCard(CardData data)
        {
            SingleCardHolder obj = Instantiate(_singleCardHolderPrefab, transform);
            obj.SetCard(data.CardSprite);
        }
    }
}