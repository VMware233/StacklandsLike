using System;
using VMFramework.Core;
using VMFramework.Procedure;

namespace VMFramework.GameLogicArchitecture
{
    public partial interface IGamePrefab : IIDOwner, INameOwner, IInitializer
    {
        public const string NULL_ID = "null";
        
        public new string id { get; set; }

        string IIDOwner<string>.id => id;

        public bool isActive { get; set; }
        
        public bool isDebugging { get; set; }
        
        public Type gameItemType { get; }
        
        public bool isPreInitializationRequired { get; }
        
        public bool isInitializationRequired { get; }
        
        public bool isPostInitializationRequired { get; }
        
        public bool isInitializationCompleteRequired { get; }
        
        public event Action<IGamePrefab, string, string> OnIDChangedEvent;

        public void CheckSettings();
    }
}
