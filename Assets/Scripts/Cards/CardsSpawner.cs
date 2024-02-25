using junglee.config;
using junglee.data;
using UnityEngine;

namespace junglee.cards
{
    public class CardsSpawner : MonoBehaviour
    {
        [SerializeField] private CardsConfig _cardsConfig;
        [SerializeField] private GroupCardsHolder _groupCardsHolderPrefab;
        [SerializeField] private Transform _cardsHolder;
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

        private void OnDestroy()
        {
            LoadDataMediator.SpawnLoadedCards -= InitializeCardHolder;
        }
    }
}