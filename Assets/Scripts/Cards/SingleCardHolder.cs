using DG.Tweening;
using junglee.config;
using System;
using UnityEngine;

namespace junglee.cards
{
    public class SingleCardHolder : MonoBehaviour
    {
        [SerializeField] private CardMediator _cardMediator;

        private RectTransform _rectT;
        private RectTransform _cardRectT;
        private CardsAligner _aligner;
        private CardData _cardData;
        private CanvasGroup _canvasGroup;

        private CardsAligner CardsAligner
        {
            get
            {
                if (_aligner == null)
                {
                    _aligner = CardsAligner.Instance;
                }
                return _aligner;
            }
        }

        public float Width => _rectT.sizeDelta.x;

        public float CardWidth => _cardRectT.sizeDelta.x;
        public CardData CardData => _cardData;

        private void Awake()
        {
            transform.localScale = new Vector3(0f, 1f, 1f);
            transform.DOScaleX(1, 0.25f);

            _rectT = transform as RectTransform;
            _cardRectT = _cardMediator.transform as RectTransform;
            _cardMediator.CardSelected += OnCardSelected;
            _cardMediator.CardDragged += OnCardDragged;
        }

        private void OnCardSelected(bool status)
        {
            if (status)
            {
                CardsAligner.AddSelectedCard(this);
            }
            else
            {
                CardsAligner.RemoveSelectedCard(this);
            }
        }

        private void OnCardDragged(bool status)
        {
            if (status)
            {
                CardsAligner.ClearSelection();
            }
            SetBlockRayCast(!status);
        }

        public void SetBlockRayCast(bool status)
        {
            _canvasGroup.blocksRaycasts = status;
        }

        public void SetCard(CardData data, float canvasSCaleFactor)
        {
            _cardMediator.SetData(data, canvasSCaleFactor);
            _cardData = data;
        }

        public void SetCanvasGroup(CanvasGroup canvasGroup)
        {
            _canvasGroup = canvasGroup;
        }

        public void RemoveHolder()
        {
            transform.DOScaleX(0, 0.25f).OnComplete(OnRemoveHolderTweenComplete);
        }

        public void OnRemoveHolderTweenComplete()
        {
            Destroy(gameObject);
        }

        public void DeselectCard()
        {
            _cardMediator.DeSelectCard();
        }

        private void OnDestroy()
        {
            _cardMediator.CardSelected -= OnCardSelected;
            _cardMediator.CardDragged -= OnCardDragged;
        }
    }
}