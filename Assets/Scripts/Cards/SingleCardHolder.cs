using DG.Tweening;
using junglee.config;
using UnityEngine;

namespace junglee.cards
{
    public class SingleCardHolder : MonoBehaviour
    {
        [SerializeField] private CardMediator _cardMediator;

        private RectTransform _rectT;
        private RectTransform _cardRectT;

        public float Width => _rectT.sizeDelta.x;

        public float CardWidth => _cardRectT.sizeDelta.x;

        private void Awake()
        {
            transform.localScale = new Vector3(0f, 1f, 1f);
            transform.DOScaleX(1, 0.25f);

            _rectT = transform as RectTransform;
            _cardRectT = _cardMediator.transform as RectTransform;
        }

        public void SetCard(CardData data)
        {
            _cardMediator.SetData(data);
        }

        public void RemoveHolder()
        {
            transform.DOScaleX(0, 0.25f).OnComplete(OnRemoveHolderTweenComplete);
        }

        public void OnRemoveHolderTweenComplete()
        {
            Destroy(gameObject);
        }
    }
}