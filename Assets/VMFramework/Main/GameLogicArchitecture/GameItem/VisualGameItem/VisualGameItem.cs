using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Property;
using VMFramework.UI;

namespace VMFramework.GameLogicArchitecture
{
    public class VisualGameItem : GameItem, IVisualGameItem
    {
        protected IDescribedGamePrefab describedGamePrefab => (IDescribedGamePrefab)gamePrefab;

        public virtual string GetTooltipTitle()
        {
            return describedGamePrefab.name;
        }

        public virtual IEnumerable<TooltipPropertyInfo> GetTooltipProperties()
        {
            foreach (var config in TooltipPropertyManager.GetTooltipPropertyConfigsRuntime(id))
            {
                string AttributeValueGetter() =>
                    $"{config.propertyConfig.name}:{config.propertyConfig.GetValueString(this)}";

                yield return new()
                {
                    attributeValueGetter = AttributeValueGetter,
                    icon = config.propertyConfig.icon,
                    isStatic = config.isStatic
                };
            }
        }

        public virtual string GetTooltipDescription()
        {
            return describedGamePrefab.description;
        }
    }
}