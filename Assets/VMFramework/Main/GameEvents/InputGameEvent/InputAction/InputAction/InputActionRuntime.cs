using Sirenix.OdinInspector;

namespace VMFramework.GameEvents
{
    public class InputActionRuntime
    {
        public InputAction inputAction;
        
        [LabelText("已经按下的时间")]
        public float heldTime = 0;

        [LabelText("是否已经触发了按压瞬间")]
        public bool hasTriggeredHoldDown = false;

        public InputActionRuntime(InputAction inputAction)
        {
            this.inputAction = inputAction;
        }
    }
}