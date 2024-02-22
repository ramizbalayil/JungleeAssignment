using junglee.cards;
using junglee.config;
using UnityEngine;

namespace junglee.data
{
    public class LoadDataMediator : MonoBehaviour
    {
        [SerializeField] private TextAsset _textAsset;

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
            CardsSpawner.Instance.InitializeCardHolder(_cardData.data);
        }
    }
}