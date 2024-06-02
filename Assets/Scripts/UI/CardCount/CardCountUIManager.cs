using System.Collections.Generic;
using StackLandsLike.Cards;
using StackLandsLike.GameCore;
using UnityEngine;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;
using VMFramework.UI;

namespace StackLandsLike.UI
{
    [ManagerCreationProvider(nameof(GameManagerType.UI))]
    public sealed class CardCountUIManager : ManagerBehaviour<CardCountUIManager>
    {
        [SerializeField]
        [UIPresetID]
        private string cardCountUIPresetID;
        
        private static readonly Dictionary<CardView, IPopupController> allPopups = new();
        
        protected override void OnBeforeInit()
        {
            base.OnBeforeInit();
            
            CardViewManager.OnCardViewCreated += OnCardViewCreated;
            CardViewManager.OnCardViewDestroyed += OnCardViewDestroyed;
        }

        private void OnCardViewCreated(CardView cardView)
        {
            if (allPopups.ContainsKey(cardView))
            {
                return;
            }
            
            var popup = PopupManager.PopupText(cardCountUIPresetID, cardView.transform, cardView.card.count);
            allPopups.Add(cardView, popup);
        }


        private void OnCardViewDestroyed(CardView cardView)
        {
            if (allPopups.Remove(cardView, out var popup) == false)
            {
                Debug.LogWarning($"CardView {cardView.name} not found in allPopups dictionary.");
                return;
            }
            
            popup.Close();
        }
    }
}