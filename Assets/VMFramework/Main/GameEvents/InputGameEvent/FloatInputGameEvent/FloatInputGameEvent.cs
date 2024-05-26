using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using VMFramework.Core;

namespace VMFramework.GameEvents
{
    public class FloatInputGameEvent : InputGameEvent<FloatInputGameEvent>, IFloatInputGameEvent, 
        IUpdateableGameEvent
    {
        private FloatInputGameEventConfig floatInputGameEventConfig => (FloatInputGameEventConfig)gamePrefab;
        
        [ShowInInspector]
        public bool isFromAxis { get; private set; }
        
        [ShowInInspector]
        public InputAxisType inputAxisType { get; private set; }
        
        [ShowInInspector]
        private List<InputActionGroupRuntime> positiveActionGroups;
        
        [ShowInInspector]
        private List<InputActionGroupRuntime> negativeActionGroups;
        
        [ShowInInspector]
        public float value { get; private set; }

        protected override void OnCreate()
        {
            base.OnCreate();

            isFromAxis = floatInputGameEventConfig.isFromAxis;
            inputAxisType = floatInputGameEventConfig.inputAxisType;
            
            positiveActionGroups = floatInputGameEventConfig.positiveActionGroups.ToRuntime().ToList();
            negativeActionGroups = floatInputGameEventConfig.negativeActionGroups.ToRuntime().ToList();
        }

        public override IEnumerable<string> GetInputMappingContent(KeyCodeToStringMode mode)
        {
            yield return "";
        }

        void IUpdateableGameEvent.Update()
        {
            if (isFromAxis)
            {
                value = inputAxisType.GetAxisValue();
            }
            else
            {
                value = 0;

                if (positiveActionGroups.Check())
                {
                    value += 1;
                }

                if (negativeActionGroups.Check())
                {
                    value -= 1;
                }
            }

            if (value != 0)
            {
                Propagate();
            }
        }
    }
}