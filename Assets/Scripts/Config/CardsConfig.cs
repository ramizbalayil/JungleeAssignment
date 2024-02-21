using UnityEngine;

namespace junglee.config
{
    [CreateAssetMenu(fileName = "CardsConfig", menuName = "Configs/Cards Config", order = 1)]
    public class CardsConfig : ScriptableObject
    {
        public CardData[] cardDatas;
    }
}