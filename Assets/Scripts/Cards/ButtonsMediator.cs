using junglee.config;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace junglee.cards
{
    public class ButtonsMediator : MonoBehaviour
    {
        [SerializeField] private Button _groupButton;
        [SerializeField] private Button _resetButton;

        private CardsAligner _aligner;

        private CardsAligner CardsAligner
        {
            get
            {
                if (_aligner == null)
                {
                    _aligner = CardsAligner.Instance;
                }
                return _aligner;
            }
        }


        private void Awake()
        {
            _groupButton.onClick.AddListener(OnGroupButtonPressed);
            _resetButton.onClick.AddListener(OnResetButtonPressed);
        }

        private void Update()
        {
            UpdateGroupButton();
        }

        private void OnResetButtonPressed()
        {
            ResetSelection();
        }

        private void OnGroupButtonPressed()
        {
            CardsAligner.CreateGroupForSelectedCards();
        }

        private void ResetSelection()
        {
            CardsAligner.ClearSelection();
        }


        private void UpdateGroupButton()
        {
            int cardCount = CardsAligner.GetSelectedCardCount();

            _groupButton.gameObject.SetActive(cardCount > 1);
            _resetButton.gameObject.SetActive(cardCount > 1);
        }
    }
}