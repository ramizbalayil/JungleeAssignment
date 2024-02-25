using junglee.cards;
using junglee.config;
using System;
using UnityEngine;

namespace junglee.data
{
    public class LoadDataMediator : MonoBehaviour
    {
        public static Action<Deck> SpawnLoadedCards;

        [SerializeField] private TextAsset _textAsset;

        private Data _cardData;

        private void Awake()
        {
            _cardData = LoadData();
        }

        private void Start()
        {
            InitializeCardHolder();
        }

        private Data LoadData()
        {
            return JsonUtility.FromJson<Data>(_textAsset.text);
        }

        private void InitializeCardHolder()
        {
            SpawnLoadedCards?.Invoke(_cardData.data);
        }
    }
}