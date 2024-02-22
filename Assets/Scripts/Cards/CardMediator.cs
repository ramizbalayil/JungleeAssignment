using DG.Tweening;
using junglee.config;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace junglee.cards
{
    [RequireComponent(typeof(Image))]
    public class CardMediator : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static Action<CardData> CardSelected;

        private Vector3 _cardOriginPosition;
        private CardData _cardData;
        private Image _cardUI;

        public CardData CardData => _cardData;

        private void Awake()
        {
            _cardOriginPosition = transform.localPosition;
            _cardUI = GetComponent<Image>();

            GroupButtonCardsAligner.CardSelectionReset += DeSelectCard;
        }

        public void SetData(CardData data)
        {
            _cardData = data;
            _cardUI.sprite = data.CardSprite;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SelectCard();
        }

        public void SelectCard()
        {
            transform.DOLocalMoveY(_cardOriginPosition.y + 50, 0.25f);
            CardSelected?.Invoke(CardData);
        }

        public void DeSelectCard()
        {
            transform.DOLocalMoveY(_cardOriginPosition.y, 0.25f);
        }

        private void OnDestroy()
        {
            GroupButtonCardsAligner.CardSelectionReset -= DeSelectCard;
        }
    }
}
