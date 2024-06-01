using System;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using VMFramework.GameEvents;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardViewTooltipManager : ManagerBehaviour<CardViewTooltipManager>, IManagerBehaviour
    {
        public void OnInitComplete(Action onDone)
        {
            ColliderMouseEventManager.AddCallback(MouseEventType.PointerEnter, OnPointerEnter);
            ColliderMouseEventManager.AddCallback(MouseEventType.PointerExit, OnPointerLeave);
            IGameItem.OnGameItemDestroyed += OnGameItemDestroyed;
            
            onDone();
        }

        private void OnGameItemDestroyed(IGameItem gameItem)
        {
            if (gameItem is ICard card)
            {
                TooltipManager.Close(card);
            }
        }

        private void OnPointerEnter(ColliderMouseEvent e)
        {
            var owner = e.trigger.owner;
            if (owner.TryGetComponent(out CardView cardView) == false)
            {
                return;
            }
            
            TooltipManager.Open(cardView.card, null);
        }

        private void OnPointerLeave(ColliderMouseEvent e)
        {
            var owner = e.trigger.owner;
            if (owner.TryGetComponent(out CardView cardView) == false)
            {
                return;
            }

            if (cardView.card == null)
            {
                return;
            }
            
            TooltipManager.Close(cardView.card);
        }
    }
}