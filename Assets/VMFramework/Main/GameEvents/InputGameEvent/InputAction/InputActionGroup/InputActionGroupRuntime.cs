using System.Collections.Generic;

namespace VMFramework.GameEvents
{
    public struct InputActionGroupRuntime
    {
        public List<InputActionRuntime> actions;

        public InputActionGroupRuntime(InputActionGroup inputActionGroup)
        {
            actions = new();

            foreach (var action in inputActionGroup.actions)
            {
                actions.Add(new(action));
            }
        }
    }
}