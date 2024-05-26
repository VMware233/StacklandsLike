using System;
using VMFramework.Core;

namespace VMFramework.UI
{
    public class UIToolkitUniqueTracingUIPanelController<T> : UIToolkitTracingUIPanelController
        where T : UIToolkitUniqueTracingUIPanelController<T>
    {
        protected static T instance { get; private set; }

        protected override void OnPreInit(UIPanelPreset preset)
        {
            base.OnPreInit(preset);

            preset.isUnique.AssertIsTrue(nameof(preset.isUnique));

            if (instance != null)
            {
                throw new Exception($"{typeof(T)}已经存在，不应该再次创建");
            }

            instance = this as T;
        }
    }
}