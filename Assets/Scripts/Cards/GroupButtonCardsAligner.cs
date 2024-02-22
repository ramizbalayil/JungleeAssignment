using junglee.config;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace junglee.cards
{
    public class GroupButtonCardsAligner : MonoBehaviour
    {
        public static Action<CardData> CardRemoved;
        public static Action CardSelectionReset;

        [SerializeField] private Button _groupButton;
        [SerializeField] private Button _resetButton;


        private List<CardData> _selectedcards;
        private CardsSpawner _spawner;
        private CardsSpawner CardsSpawner
        {
            get
            {
                if (_spawner == null)
                {
                    _spawner = CardsSpawner.Instance;
                }
                return _spawner;
            }
        }

        private void Awake()
        {
            _selectedcards = new List<CardData>();
            CardMediator.CardSelected += OnCardSelected;
            _groupButton.onClick.AddListener(OnGroupButtonPressed);
            _resetButton.onClick.AddListener(OnResetButtonPressed);

            UpdateGroupButton();
        }

        private void OnResetButtonPressed()
        {
            CardSelectionReset?.Invoke();
            ResetSelection();
        }

        private void OnGroupButtonPressed()
        {
            foreach (CardData selectedcard in _selectedcards)
            {
                CardRemoved?.Invoke(selectedcard);
            }

            CardsSpawner.SpawnCards(_selectedcards);
            ResetSelection();
        }

        private void ResetSelection()
        {
            _selectedcards.Clear();
            UpdateGroupButton();
        }

        private void OnCardSelected(CardData cardData)
        {
            _selectedcards.Add(cardData);
            UpdateGroupButton();
        }

        private void UpdateGroupButton()
        {
            _groupButton.gameObject.SetActive(_selectedcards.Count > 1);
            _resetButton.gameObject.SetActive(_selectedcards.Count > 1);
        }
    }
}