using System;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    [GamePrefabTypeAutoRegister(ID)]
    public class ColliderMouseEventConfig : GameEventConfig
    {
        public const string ID = "collider_mouse_event";
        
        public override Type gameItemType => typeof(ColliderMouseEvent);
    }
}