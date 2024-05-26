using Sirenix.OdinInspector;
using UnityEngine;
using VMFramework.Core;
using VMFramework.Localization;

namespace VMFramework.Configuration
{
    public class ColorCircularSelectChooserConfig : CircularSelectChooserConfig<Color>
    {
        [LabelText("颜色显示格式")]
        [EnumToggleButtons]
        public ColorStringFormat colorStringFormat = ColorStringFormat.Name;

        protected override string ValueToString(Color value)
        {
            return value.ToLocalizedString(colorStringFormat);
        }
    }
}