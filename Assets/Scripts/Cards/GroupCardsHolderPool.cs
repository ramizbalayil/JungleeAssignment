using junglee.utils;
using UnityEngine;
using UnityEngine.Pool;

namespace junglee.cards
{
    public class GroupCardsHolderPool : Singleton<GroupCardsHolderPool>
    {
        [SerializeField] private GroupCardsHolder _groupCardsHolderPrefab;
        [SerializeField] private Transform _cardsHolder;

        [SerializeField] private int _defaultCapacity = 10;
        [SerializeField] private int _maxCapacity = 15;

        private IObjectPool<GroupCardsHolder> _objectPool;

        protected override void Awake()
        {
            base.Awake();
            _objectPool = new ObjectPool<GroupCardsHolder>(CreateObject, OnGetFromPool,
                OnReleaseToPool, OnDestroyPooledObject, false, _defaultCapacity, _maxCapacity);
        }

        public GroupCardsHolder GetGroupHolder()
        {
            return _objectPool.Get();
        }


        private GroupCardsHolder CreateObject()
        {
            GroupCardsHolder holder = Instantiate(_groupCardsHolderPrefab, _cardsHolder);
            holder.SetPool(_objectPool);

            return holder;
        }

        private void OnGetFromPool(GroupCardsHolder holder)
        {
            holder.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(GroupCardsHolder holder)
        {
            holder.gameObject.SetActive(false);
        }

        private void OnDestroyPooledObject(GroupCardsHolder holder)
        {
            Destroy(holder.gameObject);
        }
    }
}