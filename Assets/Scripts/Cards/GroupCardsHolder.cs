using junglee.config;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace junglee.cards
{
    public class GroupCardsHolder : MonoBehaviour, IDropHandler
    {
        [SerializeField] private SingleCardHolder _singleCardHolderPrefab;

        private RectTransform _rectT;
        private Dictionary<CardData, SingleCardHolder> _cardsInGroup;
        private CardsAligner _aligner;
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

        private void Awake()
        {
            _rectT = transform as RectTransform;
            _cardsInGroup = new Dictionary<CardData, SingleCardHolder>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void RefreshWidth()
        {
            SingleCardHolder card = transform.GetChild(0).GetComponent<SingleCardHolder>();
            float width = 0f;
            float obj_width = card.Width;
            
            width += (obj_width * _cardsInGroup.Count) + (card.CardWidth - obj_width);

            _rectT.sizeDelta = new Vector2(width, _rectT.sizeDelta.y);
        }

        public void AddCard(CardData data, Canvas canvas, Transform draggableCardHolder)
        {
            if (!_cardsInGroup.ContainsKey(data))
            {
                SingleCardHolder obj = Instantiate(_singleCardHolderPrefab, transform);
                _cardsInGroup.Add(data, obj);
                obj.SetCard(data, canvas, draggableCardHolder);
                obj.SetCanvasGroup(_canvasGroup);
            }
        }

        public void MoveCard(SingleCardHolder obj)
        {
            if (!_cardsInGroup.ContainsKey(obj.CardData))
            {
                _cardsInGroup.Add(obj.CardData, obj);
                obj.SetCanvasGroup(_canvasGroup);
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

        public bool HasCard(SingleCardHolder singleCard)
        {
            return _cardsInGroup.ContainsKey(singleCard.CardData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                SingleCardHolder singleCardHolder = eventData.pointerDrag.GetComponent<CardMediator>().CardHolder;
                CardsAligner.AlignDraggedCard(this, singleCardHolder);
            }
        }
    }
}