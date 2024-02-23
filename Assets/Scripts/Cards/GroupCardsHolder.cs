using DG.Tweening;
using junglee.config;
using System.Collections.Generic;
using UnityEngine;

namespace junglee.cards
{
    public class GroupCardsHolder : MonoBehaviour
    {
        [SerializeField] private SingleCardHolder _singleCardHolderPrefab;

        private RectTransform _rectT;
        private Dictionary<CardData, SingleCardHolder> _cardsInGroup;

        private void Awake()
        {
            _rectT = transform as RectTransform;
            _cardsInGroup = new Dictionary<CardData, SingleCardHolder>();
        }

        public void RefreshWidth()
        {
            SingleCardHolder card = transform.GetChild(0).GetComponent<SingleCardHolder>();
            float width = 0f;
            float obj_width = card.Width;
            
            width += (obj_width * _cardsInGroup.Count) + (card.CardWidth - obj_width);

            _rectT.sizeDelta = new Vector2(width, _rectT.sizeDelta.y);
        }

        public void AddCard(CardData data)
        {
            if (!_cardsInGroup.ContainsKey(data))
            {
                SingleCardHolder obj = Instantiate(_singleCardHolderPrefab, transform);
                _cardsInGroup.Add(data, obj);
                obj.SetCard(data);
            }
        }

        public void MoveCard(SingleCardHolder obj)
        {
            if (!_cardsInGroup.ContainsKey(obj.CardData))
            {
                _cardsInGroup.Add(obj.CardData, obj);
            }
        }

        public void RemoveCard(SingleCardHolder obj)
        {
            if (_cardsInGroup.ContainsKey(obj.CardData))
            {
                _cardsInGroup.Remove(obj.CardData);

                if (_cardsInGroup.Count != 0)
                {
                    RefreshWidth();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}