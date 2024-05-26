using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.OdinExtensions;

namespace VMFramework.GameLogicArchitecture
{
    [HideDuplicateReferenceBox]
    [HideReferenceObjectPicker]
    [PreviewComposite]
    public abstract partial class GameItem : IGameItem
    {
        #region Properties & Fields

        [LabelText("GamePrefab")]
        [ShowInInspector]
        protected IGameTypedGamePrefab gamePrefab { get; private set; }
        
        [LabelText("ID")]
        [ShowInInspector, DisplayAsString]
        public string id => gamePrefab.id;

        public string name => gamePrefab.name;
        
        [ShowInInspector]
        public bool isDebugging => gamePrefab.isDebugging;
        
        [LabelText("游戏种类")]
        [ShowInInspector]
        public IReadOnlyGameTypeSet gameTypeSet => gamePrefab.gameTypeSet;
    
        [LabelText("唯一游戏种类")]
        [ShowInInspector]
        public GameType uniqueGameType => gamePrefab.uniqueGameType;

        public bool isDestroyed { get; private set; } = false;

        #endregion
    
        #region Interface Implementation

        IGameTypedGamePrefab IGameItem.origin
        {
            get => gamePrefab;
            set => gamePrefab = value;
        }
        
        void IGameItem.OnCreateGameItem()
        {
            OnCreate();
        }

        void IGameItem.OnClone(IGameItem other)
        {
            OnClone(other);
        }

        bool IGameItem.isDestroyed
        {
            get => isDestroyed;
            set => isDestroyed = value;
        }

        #endregion

        #region Clone

        protected virtual void OnClone(IGameItem other)
        {
            
        }

        #endregion

        #region Create & Destroy

        protected virtual void OnCreate()
        {
            // Debug.LogError(233);
        }

        void IGameItem.OnDestroyGameItem()
        {
            OnDestroy();
        }

        protected virtual void OnDestroy()
        {
            
        }

        #endregion

        #region To String

        protected virtual IEnumerable<(string propertyID, string propertyContent)> OnGetStringProperties()
        {
            yield break;
        }

        public override string ToString()
        {
            var extraString = OnGetStringProperties()
                .Select(property => property.propertyID + ":" + property.propertyContent)
                .Join(", ");

            return $"[{GetType()}:id:{id},{extraString}]";
        }

        #endregion
    }
}
