using junglee.config;
using junglee.data;
using System.Collections.Generic;
using UnityEngine;

namespace junglee.cards
{
    public class CardsSpawner : MonoBehaviour
    {
        private static CardsSpawner instance;

        public static CardsSpawner Instance => instance;

        [SerializeField] private CardsConfig _cardsConfig;
        [SerializeField] private GroupCardsHolder _groupCardsHolderPrefab;
        [SerializeField] private Transform _cardsHolder;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }
            Destroy(gameObject);
        }

        public void SpawnCards(List<CardData> cardDatas)
        {
            GroupCardsHolder groupCardsHolder = Instantiate(_groupCardsHolderPrefab, _cardsHolder);

            foreach (CardData cardData in cardDatas)
            {
                groupCardsHolder.AddCard(cardData);
            }

            groupCardsHolder.RefreshWidth();
        }

        public void InitializeCardHolder(Deck deck)
        {
            SpawnCards(GetDeckData(deck));
        }

        private List<CardData> GetDeckData(Deck deck)
        {
            List<CardData> data = new List<CardData>();

            foreach (string cardId in deck.deck)
            {
                data.Add(GetCardData(cardId));
            }
            return data;
        }

        private CardData GetCardData(string cardId)
        {
            foreach (CardData cardData in _cardsConfig.cardDatas)
            {
                if (cardData.CardId == cardId) return cardData;
            }
            return null;
        }
    }
}