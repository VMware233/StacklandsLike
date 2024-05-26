using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.GameEvents
{
    public class BoolInputGameEvent : InputGameEvent<BoolInputGameEvent>, IBoolInputGameEvent, 
        IUpdateableGameEvent
    {
        private BoolInputGameEventConfig boolInputGameEventConfig => (BoolInputGameEventConfig)gamePrefab;

        [ShowInInspector]
        private List<InputActionGroupRuntime> actionGroups;

        [ShowInInspector]
        public bool value { get; private set; } = false;

        protected override void OnCreate()
        {
            base.OnCreate();

            actionGroups = boolInputGameEventConfig.actionGroups.ToRuntime().ToList();
        }

        public IList<IList<InputAction>> GetActionGroups()
        {
            var actionGroups = new List<IList<InputAction>>();

            foreach (var actionGroup in this.actionGroups)
            {
                actionGroups.Add(actionGroup.actions.Select(action => action.inputAction).ToList());
            }
            
            return actionGroups;
        }
        
        public override IEnumerable<string> GetInputMappingContent(KeyCodeToStringMode mode)
        {
            if (actionGroups.Count == 0)
            {
                yield return "";
                yield break;
            }

            foreach (var actionGroup in actionGroups)
            {
                var contentList = new List<string>();

                foreach (var action in actionGroup.actions)
                {
                    var keyCode = action.inputAction.keyCode;
                    contentList.Add(
                        GameCoreSetting.gameEventGeneralSetting.GetKeyCodeName(keyCode, mode));
                }

                yield return "+".Join(contentList);
            }
        }

        void IUpdateableGameEvent.Update()
        {
            value = false;
            
            if (actionGroups.Check())
            {
                value = true;
                Propagate();
            }
        }
    }
}