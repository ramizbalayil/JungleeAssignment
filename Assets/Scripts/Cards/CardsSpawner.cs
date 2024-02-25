using junglee.config;
using junglee.data;
using UnityEngine;

namespace junglee.cards
{
    public class CardsSpawner : MonoBehaviour
    {
        [SerializeField] private CardsConfig _cardsConfig;
        [SerializeField] private Transform _draggableCardHolder;
        [SerializeField] private Canvas _canvas;

        private void Awake()
        {
            LoadDataMediator.SpawnLoadedCards += InitializeCardHolder;
        }

        public void InitializeCardHolder(Deck deck)
        {
            GroupCardsHolder groupCardsHolder = GroupCardsHolderPool.Instance.GetGroupHolder();

            foreach (string cardID in deck.deck)
            {
                CardData cardData = GetCardData(cardID);
                groupCardsHolder.AddCard(cardData, _canvas, _draggableCardHolder);
            }

            groupCardsHolder.RefreshWidth();
        }

        private CardData GetCardData(string cardId)
        {
            foreach (CardData cardData in _cardsConfig.cardDatas)
            {
                if (cardData.CardId == cardId) return cardData;
            }
            return null;
        }

        private void OnDestroy()
        {
            LoadDataMediator.SpawnLoadedCards -= InitializeCardHolder;
        }
    }
}