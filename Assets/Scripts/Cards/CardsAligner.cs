using junglee.utils;
using System.Collections.Generic;

namespace junglee.cards
{
    public class CardsAligner : Singleton<CardsAligner>
    {
        private List<SingleCardHolder> _selectedcards;

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

        protected override void Awake()
        {
            base.Awake();
            _selectedcards = new List<SingleCardHolder>();
        }

        public void AddSelectedCard(SingleCardHolder card)
        {
            _selectedcards.Add(card);
        }

        public void RemoveSelectedCard(SingleCardHolder card)
        {
            _selectedcards.Remove(card);
            card.DeselectCard();
        }

        public void ClearSelection()
        {
            foreach (SingleCardHolder card in _selectedcards)
            {
                card.DeselectCard();
            }
            _selectedcards.Clear();
        }

        public int GetSelectedCardCount()
        {
            return _selectedcards.Count;
        }

        public void CreateGroupForSelectedCards()
        {
            GroupCardsHolder newParent = CardsSpawner.SpawnGroupCardsHolder();

            foreach (SingleCardHolder singleCard in _selectedcards)
            {
                MoveCardToNewGroup(newParent, singleCard);
            }

            newParent.RefreshWidth();
            ClearSelection();
        }

        private void MoveCardToNewGroup(GroupCardsHolder newGroup, SingleCardHolder singleCard)
        {
            GroupCardsHolder oldParent = singleCard.transform.parent.GetComponent<GroupCardsHolder>();

            singleCard.transform.SetParent(newGroup.transform);

            newGroup.MoveCard(singleCard);

            oldParent.RemoveCard(singleCard);
        }

        public void AlignDraggedCard(GroupCardsHolder groupHolder, SingleCardHolder draggedCard)
        {
            if (groupHolder.HasCard(draggedCard)) return;

            draggedCard.SetBlockRayCast(true);
            MoveCardToNewGroup(groupHolder, draggedCard);
            groupHolder.RefreshWidth();
        }
    }

}