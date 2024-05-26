#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.ValueResolvers;
using VMFramework.Localization;

namespace VMFramework.OdinExtensions
{
    public class KeyNameAttributeDrawer : GeneralValueDropdownAttributeDrawer<KeyNameAttribute>
    {
        private ValueResolver<string> tableNameResolver;
        
        protected override void Initialize()
        {
            tableNameResolver = ValueResolver.GetForString(Property, Attribute.TableName);
            
            base.Initialize();
        }

        protected override IEnumerable<ValueDropdownItem> GetValues()
        {
            var tableName = tableNameResolver.GetValue();

            if (StringTableCollectionUtility.TryGetStringTableCollection(tableName, out var collection) == false)
            {
                return Enumerable.Empty<ValueDropdownItem>();
            }

            return collection.GetKeys().ToValueDropdownItems();
        }

        protected override void DrawCustomButtons()
        {
            base.DrawCustomButtons();

            var value = Property.ValueEntry.WeakSmartValue;

            if (value is not string key)
            {
                return;
            }
            
            var tableName = tableNameResolver.GetValue();

            if (StringTableCollectionUtility.TryGetStringTableCollection(tableName, out var collection) == false)
            {
                return;
            }

            if (collection.ExitsKey(key))
            {
                return;
            }
            
            if (Button("Create New Key", SdfIconType.Plus))
            {
                collection.CreateKey(key, "");
            }
        }
    }
}
#endif