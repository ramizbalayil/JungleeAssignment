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

            GroupButtonCardsAligner.CardRemoved += OnCardRemoved;
        }

        private void OnCardRemoved(CardData cardData)
        {
            if (_cardsInGroup.TryGetValue(cardData, out SingleCardHolder holder))
            {
                _cardsInGroup.Remove(cardData);
                RemoveHolder(holder.transform, () => DestroySingleCardHolder(holder));

                if (_cardsInGroup.Count != 0)
                {
                    RefreshWidth(false);
                }
            }
        }

        public void DestroySingleCardHolder(SingleCardHolder holder)
        {
            Destroy(holder.gameObject);
            if (_cardsInGroup.Count == 0)
            {
                RemoveHolder(transform, () => Destroy(gameObject));
            }
        }

        public void RemoveHolder(Transform tr, TweenCallback onComplete)
        {
            tr.DOScaleX(0, 0.25f).OnComplete(onComplete);
        }

        public void RefreshWidth(bool skipTween)
        {
            SingleCardHolder card = transform.GetChild(0).GetComponent<SingleCardHolder>();
            float width = 0f;
            float obj_width = card.Width;
            
            width += (obj_width * _cardsInGroup.Count) + (card.CardWidth - obj_width);

            if (skipTween)
            {
                _rectT.sizeDelta = new Vector2(width, _rectT.sizeDelta.y);
            }
            else
            {
                _rectT.DOSizeDelta(new Vector2(width, _rectT.sizeDelta.y), 0.25f);
            }

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

        private void OnDestroy()
        {
            GroupButtonCardsAligner.CardRemoved -= OnCardRemoved;
        }
    }
}