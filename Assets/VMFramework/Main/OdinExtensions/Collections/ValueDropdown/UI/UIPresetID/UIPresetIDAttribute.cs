using System;
using VMFramework.UI;

namespace VMFramework.OdinExtensions
{
    public class UIPresetIDAttribute : GamePrefabIDAttribute
    {
        public bool? IsUnique = null;

        public UIPresetIDAttribute(params Type[] uiPrefabTypes) : base(uiPrefabTypes.Length == 0
            ? new[] { typeof(IUIPanelPreset) }
            : uiPrefabTypes)
        {

        }

        public UIPresetIDAttribute() : this(typeof(IUIPanelPreset))
        {
            
        }

        public UIPresetIDAttribute(bool isUnique, params Type[] uiPrefabTypes) : this(uiPrefabTypes)
        {
            IsUnique = isUnique;
        }
    }
}