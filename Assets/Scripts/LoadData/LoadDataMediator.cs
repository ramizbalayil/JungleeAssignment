using junglee.cards;
using junglee.config;
using UnityEngine;

namespace junglee.data
{
    public class LoadDataMediator : MonoBehaviour
    {
        [SerializeField] private TextAsset _textAsset;
        [SerializeField] private CardsConfig _cardsConfig;
        [SerializeField] private GroupCardsHolder _groupCardsHolderPrefab;
        [SerializeField] private Transform _cardsHolder;

        private Data _cardData;

        private void Awake()
        {
            _cardData = LoadData();
            InitializeCardHolder();
        }

        private Data LoadData()
        {
            return JsonUtility.FromJson<Data>(_textAsset.text);
        }

        private void InitializeCardHolder()
        {
            GroupCardsHolder groupCardsHolder = Instantiate(_groupCardsHolderPrefab, _cardsHolder);

            foreach (string cardId in _cardData.data.deck)
            {
                CardData cardData = GetCardData(cardId);
                groupCardsHolder.AddCard(cardData);
            }

            groupCardsHolder.SetWidth();
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