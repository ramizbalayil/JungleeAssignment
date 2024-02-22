using junglee.config;
using System;
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

            GroupButtonCardsAligner.CardRemoved += OnCardRemoved;
        }

        private void OnCardRemoved(CardData cardData)
        {
            if (_cardsInGroup.TryGetValue(cardData, out SingleCardHolder holder))
            {
                _cardsInGroup.Remove(cardData);
                holder.RemoveHolder();
            }
            RefreshWidth();
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
            SingleCardHolder obj = Instantiate(_singleCardHolderPrefab, transform);
            _cardsInGroup.Add(data, obj);
            obj.SetCard(data);
        }
    }
}