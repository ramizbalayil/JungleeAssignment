using junglee.config;
using junglee.data;
using junglee.utils;
using UnityEngine;

namespace junglee.cards
{
    public class CardsSpawner : Singleton<CardsSpawner>
    {
        [SerializeField] private CardsConfig _cardsConfig;
        [SerializeField] private GroupCardsHolder _groupCardsHolderPrefab;
        [SerializeField] private Transform _cardsHolder;
        [SerializeField] private Transform _draggableCardHolder;
        [SerializeField] private Canvas _canvas;

        protected override void Awake()
        {
            base.Awake();

            LoadDataMediator.SpawnLoadedCards += InitializeCardHolder;
        }

        public void InitializeCardHolder(Deck deck)
        {
            GroupCardsHolder groupCardsHolder = SpawnGroupCardsHolder();

            foreach (string cardID in deck.deck)
            {
                CardData cardData = GetCardData(cardID);
                groupCardsHolder.AddCard(cardData, _canvas, _draggableCardHolder);
            }

            groupCardsHolder.RefreshWidth();
        }

        public GroupCardsHolder SpawnGroupCardsHolder()
        {
            return Instantiate(_groupCardsHolderPrefab, _cardsHolder);
        }

        private CardData GetCardData(string cardId)
        {
            foreach (CardData cardData in _cardsConfig.cardDatas)
            {
                if (cardData.CardId == cardId) return cardData;
            }
            return null;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            LoadDataMediator.SpawnLoadedCards -= InitializeCardHolder;
        }
    }
}