using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using StackLandsLike.GameCore;
using VMFramework.GameLogicArchitecture;
using VMFramework.OdinExtensions;
using VMFramework.Procedure;

namespace StackLandsLike.Cards
{
    [ManagerCreationProvider(nameof(GameManagerType.Card))]
    public sealed partial class CardManager : ManagerBehaviour<CardManager>
    {
        
    }
}