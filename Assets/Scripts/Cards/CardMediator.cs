using DG.Tweening;
using junglee.config;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace junglee.cards
{
    [RequireComponent(typeof(Image))]
    public class CardMediator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Action<bool> CardSelected;
        public Action<bool> CardDragged;

        private Vector3 _cardOriginPosition;
        private CardData _cardData;
        private Image _cardUI;
        private bool _isDragging;
        private bool _isSelected;
        private RectTransform _rectT;
        private Canvas _canvas;
        private Transform _draggableCardHolder;
        private SingleCardHolder _cardHolder;

        public CardData CardData => _cardData;
        public SingleCardHolder CardHolder => _cardHolder;

        private void Awake()
        {
            _cardOriginPosition = transform.localPosition;
            _cardUI = GetComponent<Image>();
            _rectT = GetComponent<RectTransform>();
            _cardHolder = transform.parent.GetComponent<SingleCardHolder>();

            _isDragging = false;
            _isSelected = false;
        }

        public void SetData(CardData data, Canvas canvas, Transform draggableCardHolder)
        {
            _cardData = data;
            _cardUI.sprite = data.CardSprite;
            _canvas = canvas;
            _draggableCardHolder = draggableCardHolder;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;

            CardDragged?.Invoke(_isDragging);
            CreateGroupsHolder.ShowCreateGroupHolder?.Invoke(_isDragging);

            transform.SetParent(_draggableCardHolder);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectT.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;

            CardDragged?.Invoke(_isDragging);
            CreateGroupsHolder.ShowCreateGroupHolder?.Invoke(_isDragging);

            transform.SetParent(_cardHolder.transform);
            ResetCardPosition();
        }

        public void OnPointerDown(PointerEventData eventData)
        {

        }

        public void SelectCard()
        {
            _isSelected = true;
            transform.DOLocalMoveY(_cardOriginPosition.y + 50, 0.25f);
        }

        public void DeSelectCard()
        {
            _isSelected = false;
            transform.DOLocalMoveY(_cardOriginPosition.y, 0.25f);
        }

        private void ResetCardPosition()
        {
            transform.DOLocalMove(_cardOriginPosition, 0.25f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isDragging && !_isSelected)
            {
                SelectCard();
                CardSelected?.Invoke(_isSelected);
            }
            else if (!_isDragging && _isSelected)
            {
                DeSelectCard();
                CardSelected?.Invoke(_isSelected);
            }
        }
    }
}
