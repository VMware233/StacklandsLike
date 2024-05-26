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

        #region Property

        [ShowInInspector]
        private Dictionary<string, PropertyOfGameItem> properties;

        private void GenerateProperties()
        {
            properties = new();

            var configs = TooltipPropertyManager.GetTooltipPropertyConfigsRuntime(id);

            foreach (var config in configs)
            {
                var property = PropertyOfGameItem.Create(config.propertyConfig.id, this);

                properties.Add(property.id, property);
            }
        }

        [Button("获得属性")]
        public IEnumerable<PropertyOfGameItem> GetAllProperties()
        {
            if (properties == null)
            {
                GenerateProperties();
            }

            return properties.Values;
        }

        public PropertyOfGameItem GetProperty(string propertyID)
        {
            if (properties == null)
            {
                GenerateProperties();
            }

            return properties.GetValueOrDefault(propertyID);
        }

        public PropertyOfGameItem GetPropertyStrictly(string propertyID)
        {
            var property = GetProperty(propertyID);

            if (property == null)
            {
                throw new KeyNotFoundException($"无法获取属性{propertyID}");
            }

            return property;
        }

        #endregion
    }
}