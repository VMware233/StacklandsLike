#if UNITY_EDITOR
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using VMFramework.Core;
using VMFramework.GameLogicArchitecture;

namespace VMFramework.Configuration
{
    public static class DictionaryConfigsNameListUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ValueDropdownItem> GetNameList<TID, TConfig>(
            this IDictionaryConfigs<TID, TConfig> dictionaryConfigs)
            where TConfig : IConfig, IIDOwner<TID>
        {
            foreach (var config in dictionaryConfigs.GetAllConfigs())
            {
                string text;
                if (config is INameOwner nameOwner)
                {
                    text = nameOwner.name;
                }
                else
                {
                    text = config.id.ToString();
                }

                yield return new(text, config.id);
            }
        }
    }
}
#endif