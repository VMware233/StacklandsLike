using System.Runtime.CompilerServices;
using UnityEngine;

namespace StackLandsLike.UI
{
    public static class CardViewUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetGroupPosition(this CardView cardView)
        {
            return cardView.card.group.GetPosition();
        }
    }
}