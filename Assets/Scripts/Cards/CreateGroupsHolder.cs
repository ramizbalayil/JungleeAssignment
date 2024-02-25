using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace junglee.cards
{
    public class CreateGroupsHolder : MonoBehaviour, IDropHandler
    {
        public static Action<bool> ShowCreateGroupHolder;

        [SerializeField] private Image _newGroupImage;

        private CardsAligner _aligner;

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
            ShowCreateGroupHolder += OnShowCreateGroupHolder;
            OnShowCreateGroupHolder(false);
        }

        private void OnShowCreateGroupHolder(bool status)
        {
            _newGroupImage.gameObject.SetActive(status);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                SingleCardHolder singleCardHolder = eventData.pointerDrag.GetComponent<CardMediator>().CardHolder;
                CardsAligner.AlignDraggedCard(null, singleCardHolder);
            }
        }

        private void OnDestroy()
        {
            ShowCreateGroupHolder -= OnShowCreateGroupHolder;
        }
    }
}