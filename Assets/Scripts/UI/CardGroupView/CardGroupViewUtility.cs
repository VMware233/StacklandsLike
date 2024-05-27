using System.Runtime.CompilerServices;
using StackLandsLike.Cards;

namespace StackLandsLike.UI
{
    public static class CardGroupViewUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RearrangeCardViews(this CardGroup cardGroup, bool isInstant)
        {
            if (cardGroup == null)
            {
                return;
            }
            
            if (cardGroup.TryGetComponent(out CardGroupView cardGroupView))
            {
                cardGroupView.RearrangeCardViews(isInstant);
            }
        }
    }
}